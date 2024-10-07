using System;
using System.Collections.Generic;

namespace PoolsContainer.Core
{
    public class Container<T> where T : PoolObject
    {
        public T[] Objects => _objects.ToArray();

        public event Action<T> OnCreated;

        private List<IPoolObjectCreator<T>> _creators;
        private List<T> _objects;

        public Container()
        {
            _creators = new List<IPoolObjectCreator<T>>();
            _objects = new List<T>();
        }

        public Container(params IPoolObjectCreator<T>[] creators) : this() => AddCreators(creators);

        public void AddCreators(IPoolObjectCreator<T>[] creators)
        {
            for (int i = 0; i < creators.Length; i++)
                AddCreator(creators[i]);
        }

        public void AddCreator(IPoolObjectCreator<T> creator)
        {
            _creators.Add(creator);
            creator.OnCreated += OnCreated;
            creator.OnCreated += OnObjectCreated;
        }

        public void Clear()
        {
            for (int i = 0; i < _creators.Count; i++)
            {
                _creators.Add(_creators[i]);
                _creators[i].OnCreated -= OnCreated;
                _creators[i].OnCreated -= OnObjectCreated;
            }
        }

        private void OnObjectCreated(T obj) => _objects.Add(obj);
    }
}