using System;

namespace Output
{
    public class ConsoleWriter : ISimpleWriter
    {
        public void Write(string str)
        {
            Console.WriteLine(str);
        }
    }
}