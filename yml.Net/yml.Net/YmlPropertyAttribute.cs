using System;

namespace yml.Net
{
    public class YmlPropertyAttribute : Attribute
    {
        public string Name { get; set; }
        
        public YmlPropertyAttribute(string name)
        {
            Name = name;
        }
    }
}