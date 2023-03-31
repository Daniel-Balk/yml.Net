using System;

namespace yml.Net.Converters
{
    public class FloatConverter : TypeConverter
    {
        public override Type Type { get => typeof(float); }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return float.Parse(s);
        }
    }
}