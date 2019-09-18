using System.Threading;
using Tracer;

namespace ConsoleApplication
{
    public class Parent
    {
        private Child child;
        private ITracer tracer;

        public Parent(ITracer tracer)
        {
            this.tracer = tracer;
            this.child = new Child(tracer);
        }
        
        public void methodParent()
        {
            tracer.StartTrace();
            child.method();
            Thread.Sleep(150);
            child.method();
            tracer.StopTrace();
        }
    }
}