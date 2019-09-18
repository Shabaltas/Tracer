using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tracer
{
    [XmlRoot("")]
    public class SerializableDictionary : ConcurrentDictionary<int, List<Method>>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            throw null;
        }

        public void ReadXml(XmlReader reader)
        {
             XmlSerializer keySerializer = new XmlSerializer(typeof(int));
        XmlSerializer valueSerializer = new XmlSerializer(typeof(List<Method>));

        bool wasEmpty = reader.IsEmptyElement;
        reader.Read();

        if (wasEmpty)
            return;

        while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
        {
            reader.ReadStartElement("thread");

            reader.ReadStartElement("id");
            int key = (int)keySerializer.Deserialize(reader);
            reader.ReadEndElement();

            reader.ReadStartElement("methods");
            List<Method> value = (List<Method>)valueSerializer.Deserialize(reader);
            reader.ReadEndElement();

            while (!this.TryAdd(key, value)){};

            reader.ReadEndElement();
            reader.MoveToContent();
        }
        reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(int));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(List<Method>));

            foreach (int key in this.Keys)
            {
                writer.WriteStartElement("thread");
                writer.WriteAttributeString("id", key.ToString());

                writer.WriteStartElement("methods");
                List<Method> value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
    }
}