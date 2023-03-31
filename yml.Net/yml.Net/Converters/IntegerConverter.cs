using System;

namespace yml.Net.Converters
{
    public class IntegerConverter : TypeConverter
    {
        public override Type Type { get => typeof(int); }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return int.Parse(s);
        }
    }
}