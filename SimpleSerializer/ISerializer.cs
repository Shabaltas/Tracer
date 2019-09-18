using System;

namespace SimpleSerializer
{
    public interface ISerializer<T>
    {
        string Serialize(T obj);
        T Deserialize(string str);
    }
}