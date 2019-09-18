using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Tracer
{
    public class SimpleTracer : ITracer
    {
        private readonly TraceResult TraceResult;
        private ThreadLocal<Stack<Method>> localMethodsStack = new ThreadLocal<Stack<Method>>(
            () => new Stack<Method>(), false);

        public SimpleTracer()
        {
            TraceResult = new TraceResult();
        
        }
        public void StartTrace()
        {
            StackTrace stackTrace = new StackTrace();
            Method currentMethod = new Method();
            currentMethod.InnerMethods = new List<Method>();
            currentMethod.Name = stackTrace.GetFrame(1).GetMethod().Name;
            currentMethod.ClassName = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name;

            Stack<Method> methodsStack = localMethodsStack.Value;
            ConcurrentDictionary<int, List<Method>> traceResult = TraceResult.ThreadMethods;
            int id = Thread.CurrentThread.ManagedThreadId;
            List<Method> threadMethodsList = traceResult.GetOrAdd(id, i => new List<Method>());
            if (methodsStack.Count == 0)
            {
                threadMethodsList.Add(currentMethod); 
            }
            else
            {
                Method parentMethod = methodsStack.Peek();
                Method temp = threadMethodsList[threadMethodsList.Count - 1];
                while (!parentMethod.Equals(temp))
                {
                    temp = temp.InnerMethods[temp.InnerMethods.Count - 1];
                }
                //if (temp.InnerMethods == null) temp.InnerMethods = new List<Method>();
                temp.InnerMethods.Add(currentMethod); 
            }
            methodsStack.Push(currentMethod);
            /*if (!traceResult.ContainsKey(id))
            {
                traceResult.TryAdd(id, new List<Method>(){currentMethod});
            }
            else
            {
                int nesting = stackTrace.GetFrames().Length - 3;
                List<Method> threadMethods;
                while (!traceResult.TryGetValue(id, out threadMethods)) {};
                if (nesting == 0)
                {
                    threadMethods.Add(currentMethod);
                }
                else { 
                    Method parentMethod = threadMethods[threadMethods.Count - 1];
                    while (nesting-- > 1)
                    {
                        parentMethod = parentMethod.InnerMethods[parentMethod.InnerMethods.Count - 1];
                    }
                    parentMethod.InnerMethods.Add(currentMethod);
                }   
            }*/
            currentMethod.WorkingSeconds = (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
        }

        public void StopTrace()
        {
            double after = (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
            StackTrace stackTrace = new StackTrace();
            ConcurrentDictionary<int, List<Method>> traceResult = TraceResult.ThreadMethods;
            int id = Thread.CurrentThread.ManagedThreadId;
            if (traceResult.ContainsKey(id))
            {
                Method currentMethod = localMethodsStack.Value.Pop();
                double before = currentMethod.WorkingSeconds;
                currentMethod.WorkingSeconds = after - before;
            }
        }

        public TraceResult GetTraceResult()
        {
            return TraceResult;
        }
    }
}