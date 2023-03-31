using System;

namespace yml.Net.Converters
{
    public class ByteConverter : TypeConverter
    {
        public override Type Type { get => typeof(byte); }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return byte.Parse(s);
        }
    }
}