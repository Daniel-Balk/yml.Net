using System;

namespace yml.Net.Converters
{
    public class DecimalConverter : TypeConverter
    {
        public override Type Type { get => typeof(decimal); }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return decimal.Parse(s);
        }
    }
}