using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDF.Utility
{
    [Serializable]
    public class PoolingStore
    {
        private readonly List<GameObject> _store = new List<GameObject>();
        private readonly Transform _storageParent;
        private readonly GameObject _prototype;

        public int PooledObjectsCount
        { 
            get { return _store.Count; } 
        }

        public PoolingStore(Transform storageParent, GameObject prototype, uint poolStartSize)
        {
            _storageParent = storageParent;
            _prototype = prototype;

            PopulateStore(poolStartSize);
        }
        
        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(_storageParent);
        }

        public void ReturnToPoolAll()
        {
            for (int i = 0; i < _store.Count; ++i)
            {
                ReturnToPool(_store[i]);
            }
        }

        public GameObject GetFromPool()
        {
            for (int i = 0; i < _store.Count; ++i)
            {
                if (!_store[i].activeSelf)
                {
                    return _store[i];
                }
            }

            return CreateNewObject();
        }

        private void PopulateStore(float startSize)
        {
            for (int i = 0; i < startSize; ++i)
            {
                CreateNewObject();
            }
        }

        private GameObject CreateNewObject()
        {
            var newObject = UnityEngine.Object.Instantiate(_prototype, _storageParent);
            newObject.SetActive(false);
            _store.Add(newObject);
            return newObject;
        }
    }
}