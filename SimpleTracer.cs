using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Tracer
{
    public class SimpleTracer : ITracer
    {
        private readonly ConcurrentDictionary<int, List<Method>> _traceResult;
        private readonly ThreadLocal<Stack<Method>> _localMethodsStack = new ThreadLocal<Stack<Method>>(
            () => new Stack<Method>(), false);

        public SimpleTracer()
        {
            _traceResult = new ConcurrentDictionary<int, List<Method>>();
        
        }
        public void StartTrace()
        {
            StackTrace stackTrace = new StackTrace();
            Method currentMethod = new Method();
            currentMethod.InnerMethods = new List<Method>();
            currentMethod.Name = stackTrace.GetFrame(1).GetMethod().Name;
            currentMethod.ClassName = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name;

            Stack<Method> methodsStack = _localMethodsStack.Value;
            int id = Thread.CurrentThread.ManagedThreadId;
            List<Method> threadMethodsList = _traceResult.GetOrAdd(id, i => new List<Method>());
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
                temp.InnerMethods.Add(currentMethod); 
            }
            methodsStack.Push(currentMethod);
            currentMethod.WorkingSeconds = (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
        }

        public void StopTrace()
        {
            double after = (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
            int id = Thread.CurrentThread.ManagedThreadId;
            if (_traceResult.ContainsKey(id))
            {
                Method currentMethod = _localMethodsStack.Value.Pop();
                double before = currentMethod.WorkingSeconds;
                currentMethod.WorkingSeconds = after - before;
            }
        }

        public TraceResult GetTraceResult()
        {
            TraceResult traceResult = new TraceResult(); 
            foreach (var threadsInfo in _traceResult)
            {
                traceResult.ThreadMethods.Add(threadsInfo.Key, threadsInfo.Value);
            }
            return traceResult;
        }
    }
}