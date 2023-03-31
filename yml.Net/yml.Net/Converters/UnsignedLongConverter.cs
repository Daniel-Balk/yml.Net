using System;

namespace yml.Net.Converters
{
    public class UnsignedLongConverter : TypeConverter
    {
        public override Type Type { get => typeof(ulong); }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return ulong.Parse(s);
        }
    }
}