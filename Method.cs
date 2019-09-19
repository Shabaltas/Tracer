using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tracer
{
    [Serializable]
    public class Method : IXmlSerializable
    {
        public string Name { get;  set; }
        public string ClassName { get;  set; }
        public double WorkingSeconds { get;  set; }
        public List<Method> InnerMethods { get;  set; }
   
        public override string ToString()
        {
            return "name = " + Name + ", class = " + ClassName + ", seconds = " + WorkingSeconds;
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
            XmlSerializer methodSerializer = new XmlSerializer(typeof(Method));
            writer.WriteAttributeString("name", Name);
            writer.WriteAttributeString("class", ClassName); 
            writer.WriteAttributeString("time", WorkingSeconds.ToString()); 
            foreach (var VARIABLE in InnerMethods)
            {
                methodSerializer.Serialize(writer, VARIABLE);
            }
        }
    }
}