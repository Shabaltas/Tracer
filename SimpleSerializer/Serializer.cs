using System;

namespace SimpleSerializer
{
    public interface ISerializer
    {
        void Serialize(Object obj);
        
    }
}