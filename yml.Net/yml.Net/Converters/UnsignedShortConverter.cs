using System;

namespace yml.Net.Converters
{
    public class UnsignedShortConverter : TypeConverter
    {
        public override Type Type { get => typeof(ushort); }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return ushort.Parse(s);
        }
    }
}