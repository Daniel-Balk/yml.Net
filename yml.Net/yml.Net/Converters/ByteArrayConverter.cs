using System;

namespace yml.Net.Converters
{
    public class ByteArrayConverter : TypeConverter
    {
        public override Type Type { get => typeof(byte[]); }

        public override string Serialize(object o)
        {
            var ba = (byte[]) o;
            return "!!binary |\n    " + Convert.ToBase64String(ba, Base64FormattingOptions.InsertLineBreaks).Replace("\n", "\n    ");
        }

        public override object Deserialize(string s)
        {
            var base64 = s
                .Replace("!!binary", "")
                .Replace("|", "")
                .Replace(" ", "")
                .Replace("\r", "")
                .Replace("\n", "");
            
            return Convert.FromBase64String(base64);
        }
    }
}