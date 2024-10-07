using System;

namespace PoolsContainer
{
    public interface IPoolObjectCreator<T> where T : PoolObject
    {
        public event Action<T> OnCreated;
    }
}