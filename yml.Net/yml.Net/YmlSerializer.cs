using System;
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
            if (o == null)
                return "";
            
            if (!_convertersInit)
            {
                foreach (var typeConverter in _converters)
                {
                    typeConverter.Serializer = this;
                }

                _convertersInit = true;
            }
            
            if (o is IEnumerable && !(o is string | o is byte[]))
            {
                return _converters.First(x => x.Type == typeof(IEnumerable)).Serialize(o);
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

        internal object FromString(string s, Type ty)
        {
            if (ty ==  typeof(IEnumerable) && !(ty == typeof(string) | ty == typeof(byte[])))
            {
                return _converters.First(x => x.Type == typeof(IEnumerable)).Deserialize(s);
            }

            if (!_convertersInit)
            {
                foreach (var typeConverter in _converters)
                {
                    typeConverter.Serializer = this;
                }

                _convertersInit = true;
            }

            var t = ty;
            foreach (var c in _converters)
            {
                if (c.Type == t)
                {
                    return c.Deserialize(s);
                }
            }

            s = s.Replace("\t", "    ");

            List<string> ymlblocks = new List<string>();

            var lines = s.Replace("\r", "").Split("\n");
            var buff = "";
            
            foreach (var line in lines)
            {
                if (!line.StartsWith(" ") && !line.StartsWith("-"))
                {
                    if (line.Contains(":"))
                    {
                        if (!string.IsNullOrWhiteSpace(buff.TrimEnd()))
                            ymlblocks.Add(buff.TrimEnd());
                        buff = "";
                    }
                }

                buff += line + "\n";
            }
            
            ymlblocks.Add(buff.TrimEnd());
            
            List<(string, string)> vals = new List<(string, string)>();

            foreach (var block in ymlblocks)
            {
                var name = block[..block.IndexOf(":")];
                var val = block[name.Length..][2..].Replace("\n    ", "\n").Trim();
                vals.Add((name, val));
            }

            var inst = Activator.CreateInstance(ty);
            
            var props = inst.GetType().GetProperties();

            foreach (var prop in props)
            {
                var type = prop.PropertyType;
                TypeConverter? typeConverter = null;

                foreach (var c in _converters)
                {
                    if (prop.PropertyType == typeof(IEnumerable) && !(prop.PropertyType  == typeof(string) | prop.PropertyType == typeof(byte[])))
                    {
                        typeConverter = _converters.First(x => x.Type == typeof(IEnumerable));
                        break;
                    }

                    if (c.Type == type)
                        typeConverter = c;
                    
                    if (type.IsGenericType)
                        if(c.Type == type.GetGenericTypeDefinition())
                            typeConverter = c;
                }

                var name = GetName(prop);
                var status = GetStatus(prop);

                if(!status)
                    continue;

                var index = vals.FindIndex(x => x.Item1 == name);
                
                if(index < 0)
                    continue;

                var tuple = vals[index];
                
                if (typeConverter != null)
                {
                    typeConverter.FullTargetType = prop.PropertyType;
                    var val = typeConverter.Deserialize(tuple.Item2);
                    prop.SetValue(inst, val);
                }
                else
                {                    
                    var val = FromString(tuple.Item2, prop.PropertyType);
                    prop.SetValue(inst, val);
                }
            }

            return inst;
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