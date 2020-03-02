using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CodeSnippetsAndUtils
{
    public static class GenericUtils
    {
        private static class Converter<T> where T : IConvertible
        {
            private static readonly TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            static Converter()
            {
                converter = TypeDescriptor.GetConverter(typeof(T));
                TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(converter.GetType()));
            }

            public static T ChangeType(object value)
            {
                return (T)converter.ConvertFrom(value);
            }
        }
    }
}
