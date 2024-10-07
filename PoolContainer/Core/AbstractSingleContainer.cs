using UnityEngine;
using PoolsContainer.Core;

namespace PoolsContainer.Core
{
    public abstract class AbstractSingleContainer<T> where T : PoolObject
    {
        [SerializeField] private MonoBehaviour[] poolsMono;

        public T[] Objects => _container.Objects;

        protected Container<T> _container;

        protected virtual void Awake()
        {
            _container = new Container<T>();

            for (int i = 0; i < poolsMono.Length; i++)
            {
                if (poolsMono[i].TryGetComponent(out IPoolObjectCreator<T> creator))
                {
                    _container.AddCreator(creator);
                    _container.OnCreated += Created;
                }
            }
        }

        protected virtual void OnDestroy()
        {
            _container.OnCreated -= Created;
            _container.Clear();
        }

        protected abstract void Created(T obj);
    }
}