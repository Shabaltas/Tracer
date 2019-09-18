using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Tracer
{
    public class TraceResult
    {
        public ConcurrentDictionary<int,List<Method>> ThreadMethods { get; internal set; }

        public TraceResult()
        {
            ThreadMethods = new ConcurrentDictionary<int, List<Method>>();
        }
    }
}