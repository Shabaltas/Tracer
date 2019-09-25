using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using NUnit.Framework;
using Tracer;

namespace TestProject
{
    [TestFixture]
    public class SimpleTracerTest
    {
        [Test]
        public void ShouldReturnTwoThreads()
        {
            ITracer tracer = new SimpleTracer();
            new Helper(tracer).Method2();
            Thread thread = new Thread(new Helper(tracer).Method3);
            thread.Start();
            thread.Join();
            TraceResult traceResult = tracer.GetTraceResult();
            Assert.AreEqual(2, traceResult.ThreadMethods.Keys.Count);
        }

        [Test]
        public void ShouldReturnTwoSameMethods()
        {
            ITracer tracer = new SimpleTracer();
            new Helper(tracer).Method2();
            ReadOnlyDictionary<int, List<Method>> traceResult = tracer.GetTraceResult().ThreadMethods;
            List<Method> method1InnerMethods = traceResult[Thread.CurrentThread.ManagedThreadId][0].InnerMethods; 
            Assert.AreEqual(2, method1InnerMethods.Count);
            Assert.AreEqual(method1InnerMethods[0].Name, method1InnerMethods[1].Name);
            Assert.AreEqual(method1InnerMethods[0].ClassName, method1InnerMethods[1].ClassName);
        }
        
        [Test]
        public void ShouldReturnTwoMethods()
        {
            ITracer tracer = new SimpleTracer();
            new Helper(tracer).Method1();
            ReadOnlyDictionary<int, List<Method>> traceResult = tracer.GetTraceResult().ThreadMethods;
            Assert.AreEqual(2, traceResult[Thread.CurrentThread.ManagedThreadId][0].InnerMethods.Count);
        }
        private class Helper
        {
            private ITracer _tracer;

            internal Helper(ITracer tracer)
            {
                _tracer = tracer;
            }

            internal void Method1()
            {
                _tracer.StartTrace();
                Method2();
                Method3();
                _tracer.StopTrace();
            }

            internal void Method2()
            {
                _tracer.StartTrace();
                Method3();
                Method3();
                _tracer.StopTrace();
            }

            internal void Method3()
            {
                _tracer.StartTrace();
                Thread.Sleep(4);
                _tracer.StopTrace();
            }
        }
    }
}