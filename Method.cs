using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Tracer
{
    [Serializable]
    public class Method
    {
        public string Name { get;  set; }
        public string ClassName { get;  set; }
        public double WorkingSeconds { get;  set; }
        public List<Method> InnerMethods { get;  set; }
   
        public override string ToString()
        {
            return "name = " + Name + ", class = " + ClassName + ", seconds = " + WorkingSeconds;
        }
    }
}