using System;
using System.Collections;

namespace yml.Net.Converters
{
    public class IEnumerableConverter : TypeConverter
    {
        public override Type Type { get => typeof(IEnumerable); }
        public override string Serialize(object o)
        {
            var a = (IEnumerable) o;
            string res = "";

            foreach (var v in a)
            {
                res += "\n- " + Serializer.ToString(v).Replace("\n", "\n    ");
            }

            return res;
        }

        public override object Deserialize(string s)
        {
            throw new System.NotImplementedException();
        }
    }
}