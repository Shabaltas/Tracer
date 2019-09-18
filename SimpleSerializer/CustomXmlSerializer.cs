using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SimpleSerializer
{
    public class CustomXmlSerializer<T> : ISerializer<T>
    {
        public string Serialize(T obj)
        {
            StringWriter stream = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream,obj);
            return stream.ToString();

        }

        public T Deserialize(string str)
        {
           throw new NotImplementedException();
        }
    }
}