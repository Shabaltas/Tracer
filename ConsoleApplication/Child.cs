using System.Threading;
using Tracer;

namespace ConsoleApplication
{
    public class Child
    {
        private ITracer tracer;

        public Child(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public void method()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }
    }
}