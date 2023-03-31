using System;

namespace yml.Net.Converters
{
    public class DoubleConverter : TypeConverter
    {
        public override Type Type { get => typeof(double); }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return double.Parse(s);
        }
    }
}