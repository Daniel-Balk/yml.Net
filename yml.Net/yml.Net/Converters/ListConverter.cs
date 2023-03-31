using System;
using System.Collections;
using System.Collections.Generic;

namespace yml.Net.Converters
{
    public class ListConverter : TypeConverter
    {
        public override string Serialize(object o)
        {
            var a = (IEnumerable) o;
            string res = "";

            foreach (var v in a)
            {
                res += "- " + Serializer.ToString(v).Replace("\n", "\n    ");
            }

            return res;
        }

        public override object Deserialize(string s)
        {
            throw new System.NotImplementedException();
        }

        public override Type Type
        {
            get => typeof(List<>);
        }
    }
}