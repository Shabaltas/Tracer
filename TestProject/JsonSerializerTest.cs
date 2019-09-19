using System;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleSerializer;
using Tracer;

namespace TestProject
{
    [TestFixture]
    public class JsonSerializerTest
    {
        [Test]
        public void ShouldSerializeMethodTest()
        {
            Method method = new Method("method", "ClassName", 12.8, new List<Method>());
            string expected =
                "{\"Name\":\"" + method.Name + "\",\"ClassName\":\"" + method.ClassName + "\",\"WorkingSeconds\":" + method.WorkingSeconds + ",\"InnerMethods\":null}";
            Assert.AreEqual(expected, new JsonSerializer<Method>().Serialize(method));
        }

        [Test]
        public void ShouldSerializeNullTest()
        {
            Assert.AreEqual("null", new JsonSerializer<Object>().Serialize(null));
        }

        [Test]
        public void ShouldReturnEmptyListTest()
        {
            Assert.AreEqual("[]", new JsonSerializer<List<Object>>().Serialize(new List<object>()));
        }
    }
}