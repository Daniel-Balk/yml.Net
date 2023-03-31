using System;

namespace yml.Net.Converters
{
    public class SignedByteConverter : TypeConverter
    {
        public override Type Type { get => typeof(sbyte); }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return sbyte.Parse(s);
        }
    }
}