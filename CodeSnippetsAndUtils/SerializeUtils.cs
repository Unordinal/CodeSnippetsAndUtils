using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace CodeSnippetsAndUtils
{
    public static class SerializeUtils
    {
        public static string JsonSerialize<T>(this T toSerialize)
        {
            return JsonSerializer.Serialize(toSerialize);
        }

        public static T JsonDeserialize<T>(this string toDeserialize)
        {
            return JsonSerializer.Deserialize<T>(toDeserialize);
        }

        public static string XmlSerialize<T>(this T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, toSerialize);

            return stringWriter.ToString();
        }

        public static T XmlDeserialize<T>(this string toDeserialize)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using var stringReader = new StringReader(toDeserialize);

            return (T)xmlSerializer.Deserialize(stringReader);
        }
    }
}
