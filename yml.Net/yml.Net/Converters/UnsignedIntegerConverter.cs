using System;

namespace yml.Net.Converters
{
    public class UnsignedIntegerConverter : TypeConverter
    {
        public override Type Type { get => typeof(uint); }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return uint.Parse(s);
        }
    }
}