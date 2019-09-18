using System;
using Newtonsoft.Json;

namespace SimpleSerializer
{
    public class JsonSerializer<T> : ISerializer<T>
    {
        public string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}