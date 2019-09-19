using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace Output
{
    public class FileWriter : ISimpleWriter
    {
        private static string DEFAULT_NAME = "out.txt";
        public void Write(string str)
        {
            FileStream fs = new FileStream(DEFAULT_NAME, FileMode.Create);
            byte[] array = Encoding.Default.GetBytes(str);
            fs.Write(array, 0, array.Length);
        }

        public void Write(string filename, string str)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            byte[] array = Encoding.Default.GetBytes(str);
            fs.Write(array, 0, array.Length); 
        }
    }
}