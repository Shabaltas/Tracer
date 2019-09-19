using System;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleSerializer;
using Tracer;

namespace TestProject
{
    [TestFixture]
    public class CustomXmlSerializerTest
    {
        [Test]
        public void ShouldSerializeMethodTest()
        {
            Method method = new Method("method", "ClassName", 12.8, new List<Method>());
            string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
                              "<Method name=\"" + method.Name + "\" class=\"" + method.ClassName + "\" time=\"" + method.WorkingSeconds+"\" />";
            Assert.AreEqual(expected, new CustomXmlSerializer<Method>().Serialize(method));
        }

        [Test]
        public void ShouldSerializeNullTest()
        {
            string expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
                              "<anyType xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:nil=\"true\" />";
            Assert.AreEqual(expected, new CustomXmlSerializer<object>().Serialize(null));
        }
    }
}