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
        public string Name { get; internal set; }
        public string ClassName { get; internal set; }
        public double WorkingSeconds { get; internal set; }
        public List<Method> InnerMethods { get; internal set; }

        public Method()
        {
            
        }
        public Method(string name, string className, double workingSeconds, List<Method> innerMethods)
        {
            Name = name;
            ClassName = className;
            WorkingSeconds = workingSeconds;
            InnerMethods = innerMethods;
        }
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