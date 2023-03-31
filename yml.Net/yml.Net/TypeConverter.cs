using System;

namespace yml.Net
{
    public abstract class TypeConverter
    {
        public abstract Type Type { get; }

        public abstract string Serialize(object o);
        public abstract object Deserialize(string s);
        
        public YmlSerializer Serializer { get; set; }
        public Type FullTargetType { get; set; }
    }
}