using System;

namespace yml.Net.Converters
{
    public class ShortConverter : TypeConverter
    {
        public override Type Type
        {
            get => typeof(short);
        }

        public override string Serialize(object o)
        {
            return o.ToString();
        }

        public override object Deserialize(string s)
        {
            return short.Parse(s);
        }
    }
}