using System;
using System.Linq;

namespace yml.Net.Converters
{
    public class StringConverter : TypeConverter
    {
        public override Type Type
        {
            get => typeof(String);
        }

        public override string Serialize(object o)
        {
            var s = (string) o;
            return s.Contains("\n") ? "|\n" + s.Replace("\n", "\n    ") : s;
        }

        public override object Deserialize(string s)
        {
            if (!s.Contains("\n"))
                return s;
            return s.Remove(0, 1).Replace("\n    ", "\n").Trim();
        }
    }
}