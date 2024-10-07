using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolsContainer.Core
{
    public class Pool<T> where T : PoolObject
    {
        public T Prefab { get; private set; }
        public Transform Container { get; private set; }

        protected List<T> _pool;

        protected Action<object, T> OnCreated;

        public Pool(T prefab, int count, Transform container, Action<object, T> listener)
        {
            Prefab = prefab;
            Container = container;
            OnCreated += listener;
            
            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveAfterInstance = false)
        {
            var createdObject = GameObject.Instantiate<T>(Prefab, Container);
            createdObject.gameObject.SetActive(isActiveAfterInstance);
            _pool.Add(createdObject);
            OnCreated?.Invoke(Prefab, createdObject);
            return createdObject;
        }

        public void AddToPool(T obj)
        {
            obj.transform.SetParent(Container);
            _pool.Add(obj);
            OnCreated?.Invoke(Prefab, obj);
        }
        
        public bool TryGetObjectFromPool(out T poolObject)
        {
            foreach (var obj in _pool)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    poolObject = obj;
                    if (!poolObject.gameObject.activeInHierarchy) poolObject.gameObject.SetActive(true);
                    else continue;
                    return true;
                }
            }
            poolObject = null;
            return false;
        }

        public T GetObjectFromPool(out bool isNew)
        {
            if (TryGetObjectFromPool(out var poolObject))
            {
                isNew = false;
                return poolObject;
            }
            else
            {
                isNew = true;
                return CreateObject(true);
            }
        }
    }
}