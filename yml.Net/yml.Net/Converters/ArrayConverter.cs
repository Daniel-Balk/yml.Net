using System;
using System.Collections.Generic;

namespace yml.Net.Converters
{
    public class ArrayConverter : TypeConverter
    {
        public override string Serialize(object o)
        {
            var a = (Array) o;
            string res = "";

            foreach (var v in a)
            {
                res += "- " + Serializer.ToString(v).Replace("\n", "\n    ");
            }

            return res;
        }

        public override object Deserialize(string s)
        {
            var t = FullTargetType;
            var lt = t.GenericTypeArguments[0];
            var list = Activator.CreateInstance(t);

            void Add(object obj)
            {
                var m = t.GetMethod("Add");
                m?.Invoke(list, new[] {obj});
            }

            List<string> os = new List<string>();
            
            var lines = s.Replace("\r", "").Split("\n");
            var buff = "";
            
            foreach (var line in lines)
            {
                if (!line.StartsWith(" "))
                {
                    if (line.StartsWith("-"))
                    {
                        if (!string.IsNullOrWhiteSpace(buff.TrimEnd()))
                            os.Add(buff.TrimEnd());
                        buff = "";
                    }
                }

                buff += line + "\n";
            }
            
            os.Add(buff.TrimEnd());

            foreach (var o in os)
            {
                var ob = o.Remove(0, 2).Replace("\n  ", "\n");
                var d = Serializer.FromString(ob, lt);
                Add(d);
            }

            return list.GetType().GetMethod("ToArray").Invoke(list, new object[0]);
        }

        public override Type Type
        {
            get => typeof(Array);
        }
    }
}