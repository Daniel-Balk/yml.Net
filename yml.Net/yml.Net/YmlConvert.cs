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
    }
}