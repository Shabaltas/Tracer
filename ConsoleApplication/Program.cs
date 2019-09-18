using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Tracer;

namespace ConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ITracer simpleTracer = new SimpleTracer();
            Parent parent = new Parent(simpleTracer);
            parent.methodParent();
            parent.methodParent();
            Thread thread = new Thread(parent.methodParent);
            thread.Start(); 
            thread.Join();
            TraceResult traceResult = simpleTracer.GetTraceResult();
            IEnumerator<KeyValuePair<int, List<Method>>> enumerator = traceResult.ThreadMethods.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Key + " :");
                foreach (var method in enumerator.Current.Value)
                {
                    Console.WriteLine("    " + method);
                    if (method.InnerMethods != null) print(method.InnerMethods, 2);
                }
            }
        }

        static void print(List<Method> methods, int count)
        {
            foreach (var method in methods)
            {
                for (int i = 0; i < count; i++)
                {
                    Console.Write("    ");
                }
                Console.WriteLine(method);
                if (method.InnerMethods != null && method.InnerMethods.Count != 0) 
                    print(method.InnerMethods, ++count);
            } 
        }
    }
}