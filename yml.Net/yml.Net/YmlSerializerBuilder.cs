using yml.Net.Converters;

namespace yml.Net
{
    public class YmlSerializerBuilder
    {
        private YmlSerializer _serializer = new YmlSerializer();

        public YmlSerializer Build()
        {
            return _serializer;
        }

        public YmlSerializerBuilder UseCommonTypes()
        {
            _serializer._converters.Add(new StringConverter());
            _serializer._converters.Add(new ListConverter());
            _serializer._converters.Add(new ArrayConverter());
            _serializer._converters.Add(new ByteArrayConverter());
            _serializer._converters.Add(new ByteConverter());
            _serializer._converters.Add(new DecimalConverter());
            _serializer._converters.Add(new DoubleConverter());
            _serializer._converters.Add(new FloatConverter());
            _serializer._converters.Add(new IntegerConverter());
            _serializer._converters.Add(new ListConverter());
            _serializer._converters.Add(new LongConverter());
            _serializer._converters.Add(new ShortConverter());
            _serializer._converters.Add(new SignedByteConverter());
            _serializer._converters.Add(new UnsignedIntegerConverter());
            _serializer._converters.Add(new UnsignedLongConverter());
            _serializer._converters.Add(new UnsignedShortConverter());
            _serializer._converters.Add(new IEnumerableConverter());

            return this;
        }

        public YmlSerializerBuilder UseCustomConverter(TypeConverter converter)
        {
            _serializer._converters.Add(converter);

            return this;
        }
    }
}