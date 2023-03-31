using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace yml.Net
{
    public class YmlSerializer
    {
        internal List<TypeConverter> _converters = new List<TypeConverter>();

        private bool _convertersInit = false;
        internal string ToString(object o)
        {
            if (!_convertersInit)
            {
                foreach (var typeConverter in _converters)
                {
                    typeConverter.Serializer = this;
                }

                _convertersInit = true;
            }

            var t = o.GetType();
            foreach (var c in _converters)
            {
                if (c.Type == t)
                {
                    return c.Serialize(o);
                }
            }
            
            var yml = "";
            var intends = "";

            void Intend()
            {
                intends += "    ";
            }

            void Unintend()
            {
                if (intends.Length > 3)
                {
                    intends = intends.Remove(0, 4);
                }
            }

            void ApplyLine(string line)
            {
                yml += "\n" + intends + line;
            }

            void Apply(string text)
            {
                foreach (var s in text.Split('\n'))
                {
                    ApplyLine(s);
                }
            }
            
            var props = o.GetType().GetProperties();

            foreach (var prop in props)
            {
                var type = prop.PropertyType;
                TypeConverter? typeConverter = null;

                foreach (var c in _converters)
                {
                    if (prop.GetValue(o) is IEnumerable && !(prop.GetValue(o) is string | prop.GetValue(o) is byte[]))
                    {
                        typeConverter = _converters.First(x => x.Type == typeof(IEnumerable));
                        break;
                    }
                    
                    if (c.Type == type)
                        typeConverter = c;
                }

                var name = GetName(prop);
                var status = GetStatus(prop);

                if(!status)
                    continue;

                if (typeConverter != null)
                {
                    var value = typeConverter.Serialize(prop.GetValue(o));
                    var attr = name + ": " + value;
                    Apply(attr);
                }
                else
                {
                    Apply(name + ": ");
                    Intend();
                    Apply(ToString(prop.GetValue(o)));
                    Unintend();
                }
            }

            yml = yml.TrimStart();
            return yml;
        }

        private string GetName(PropertyInfo prop)
        {
            foreach (var a in prop.GetCustomAttributes<YmlPropertyAttribute>())
            {
                return a.Name;
            }

            return prop.Name;
        }

        private bool GetStatus(PropertyInfo prop)
        {
            foreach (var a in prop.GetCustomAttributes<YmlIgnoreAttribute>())
            {
                return false;
            }

            return true;
        }
    }
}