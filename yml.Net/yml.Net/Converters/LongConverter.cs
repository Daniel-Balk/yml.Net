using System;

namespace yml.Net.Converters
{
    public class LongConverter : TypeConverter
    {
        public override Type Type { get => typeof(long); }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return long.Parse(s);
        }
    }
}