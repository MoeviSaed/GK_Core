using System;
using System.Collections;
using UnityEngine;

namespace PoolsContainer
{
    public class PoolObject : MonoBehaviour
    {
        private Coroutine _backToPoolCoroutine;

        public void BackToPool(float time)
        {
            if (gameObject.activeInHierarchy)
            {
                if (_backToPoolCoroutine != null) StopCoroutine(_backToPoolCoroutine);
                _backToPoolCoroutine = StartCoroutine(Timer(time));
            }
            else BackToPool();
        }

        public void StopTimer() => StopCoroutine(Timer());

        private IEnumerator Timer(float time = 0)
        {
            yield return new WaitForSeconds(time);
            BackToPool();
        }

        public void BackToPool()
        {
            OnBackToPool();
            if (_backToPoolCoroutine != null) StopCoroutine(_backToPoolCoroutine);
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }

        public void BackToPool(Transform parent)
        {
            transform.SetParent(parent);
            BackToPool();
        }

        protected virtual void OnBackToPool() { }
    }
}