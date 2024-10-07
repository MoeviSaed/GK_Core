using System;
using System.Collections.Generic;
using UnityEngine;
using PoolsContainer.Core;

namespace PoolsContainer.Core
{
    public abstract class MonoPool : MonoBehaviour, IObjectCreator<PoolObject>
    {
        [SerializeField] private bool createParents = true;
        [SerializeField] private Transform poolsContainer;
        [SerializeField] private int startCountForEachObject;

        protected Dictionary<object, Pool<PoolObject>> poolsMap;

        event Action<PoolObject> IObjectCreator<PoolObject>.OnCreated
        {
            add => OnPoolObjectCreated += value;
            remove => OnPoolObjectCreated -= value;
        }
        public event Action<PoolObject> OnPoolObjectCreated;

        protected virtual void Awake()
        {
            if (!poolsContainer) poolsContainer = transform;
            poolsMap = new Dictionary<object, Pool<PoolObject>>();
        }


        #region Open

        public T GetObject<T>(T prefab) where T : PoolObject => GetObject(prefab, out bool isNew);
        public T GetObject<T>(T prefab, out bool isNew) where T : PoolObject
        {
            if (poolsMap == null) poolsMap = new Dictionary<object, Pool<PoolObject>>();
            if (poolsMap.ContainsKey(prefab)) return GetFromPool(prefab, out isNew);
            else return CreatePoolAndGet(prefab, out isNew);
        }

        public void AddToPool<T>(T prefab, float backToPoolTime, params T[] objects) where T : PoolObject
        {
            if (poolsMap == null) poolsMap = new Dictionary<object, Pool<PoolObject>>();
            if (!poolsMap.ContainsKey(prefab)) poolsMap.Add(prefab, new Pool<PoolObject>(prefab, startCountForEachObject, poolsContainer, OnCreated));
            Pool<PoolObject> pool = poolsMap[prefab];

            for (int i = 0; i < objects.Length; i++)
            {
                pool.AddToPool(objects[i]);
                if (backToPoolTime != 0) objects[i].BackToPool(backToPoolTime);
            }
        }

        #endregion

        #region Close

        protected T GetFromPool<T>(T prefab, out bool isNew) where T : PoolObject
        {
            var t = poolsMap[prefab].GetObjectFromPool(out isNew) as T;
            return t;
        }

        protected T CreatePoolAndGet<T>(T prefab, out bool isNew) where T : PoolObject
        {
            Transform container;
            if (createParents)
            {
                container = new GameObject(prefab.name).transform;
                container.SetParent(poolsContainer);
                container.localPosition = Vector3.zero;
            }
            else container = poolsContainer;

            poolsMap.Add(prefab, new Pool<PoolObject>(prefab, startCountForEachObject, container, OnCreated));
            return GetFromPool(prefab, out isNew);
        }

        private void OnCreated(object prefab, PoolObject obj)
        {
            OnPoolObjectCreated?.Invoke(obj);
            Created(prefab, obj);
        }

        protected abstract void Created(object prefab, PoolObject obj);

        #endregion
    }
}