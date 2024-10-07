using System;

namespace PoolsContainer.Core
{
    public interface IObjectCreator<T> 
    {
        public event Action<T> OnCreated;
    }
}