using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Tracer
{
    public class Method
    {
        public string Name { get; internal set; }
        public string ClassName { get; internal set; }
        public double WorkingSeconds { get; internal set; }
        public List<Method> InnerMethods { get; internal set; }

        public override string ToString()
        {
            return "name = " + Name + ", class = " + ClassName + ", seconds = " + WorkingSeconds;
        }
    }
}