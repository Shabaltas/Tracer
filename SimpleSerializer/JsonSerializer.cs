using System;
using Newtonsoft.Json;

namespace SimpleSerializer
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        public string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj);
            //.Replace("{", " {\n")
            //.Replace("}", "\n}\n");
        }

        public T Deserialize(string json)
        {
            throw new NotImplementedException();
            //return JsonConvert.DeserializeObject<T>(json);
        }
    }
}