using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Tracer
{
    [Serializable]
    public class TraceResult
    {
        public SerializableDictionary ThreadMethods { get; set; }

        public TraceResult()
        {
            ThreadMethods = new SerializableDictionary();
        }
    }
}