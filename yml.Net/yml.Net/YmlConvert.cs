using System;

namespace yml.Net
{
    public class YmlConvert
    {
        static YmlConvert()
        {
            DefaultSerializer = new YmlSerializerBuilder()
                .UseCommonTypes()
                .Build();
        }
        
        public static YmlSerializer DefaultSerializer { get; set; }
        
        public static string Serialize(object obj)
        {
            return Serialize(obj, DefaultSerializer);
        }

        public static string Serialize(object obj, YmlSerializer serializer)
        {
            return serializer.ToString(obj);
        }
        
        public static T Deserialize<T>(string str)
        {
            return Deserialize<T>(str, DefaultSerializer);
        }

        public static T Deserialize<T>(string str, YmlSerializer serializer)
        {
            return (T)serializer.FromString(str, typeof(T));
        }
    }
}