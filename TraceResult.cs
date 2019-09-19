using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tracer
{
    [Serializable]
    [XmlRoot("")]
    public class TraceResult : IXmlSerializable
    {
        public Dictionary<int, List<Method>> ThreadMethods {get; }   
        internal TraceResult()
        {
            ThreadMethods = new Dictionary<int, List<Method>>();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer valueSerializer = new XmlSerializer(typeof(Method));

            foreach (int key in ThreadMethods.Keys)
            {
                writer.WriteStartElement("thread");
                writer.WriteAttributeString("id", key.ToString());
                List<Method> value = ThreadMethods[key];
                foreach (var VARIABLE in value)
                {
                    valueSerializer.Serialize(writer, VARIABLE);
                }
                writer.WriteEndElement();
            }
        }
    }
}