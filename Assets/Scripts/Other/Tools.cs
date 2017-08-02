using System.Xml.Serialization;
using System.IO;

public static class Tools
{
    public static string Serialise<T>(this T toSerialise)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringWriter writer = new StringWriter();
        xml.Serialize(writer, toSerialise);
        return writer.ToString();
    }

    public static T Deserialise<T>(this string toDeserialise)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(toDeserialise);
        return (T)xml.Deserialize(reader);
    }
}