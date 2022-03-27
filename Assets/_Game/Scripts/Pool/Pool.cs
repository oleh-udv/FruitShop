using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Pool <T> where T : MonoBehaviour
    {
        private T _prefab;
        private Transform _container;

        private List<T> _pool;

        #region Constructors
        public Pool(T prefab, Transform container, int count)
        {
            _prefab = prefab;
            _container = container;

            CreatePool(count);
        }
        #endregion

        #region PublicMethods
        public bool HasFreeElements(out T element)
        {
            foreach (var poolObject in _pool)
            {
                if (poolObject.gameObject.activeInHierarchy == false)
                {
                    element = poolObject;
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElements(out var element))
                return element;
            else
                return CreateObject();
        }
        #endregion

        #region PrivateMethods
        private void CreatePool(int count)
        {
            _pool = new List<T>(count);
            for (int i = 0; i < count; i++)
                CreateObject();
        }

        private T CreateObject()
        {
            var createdObject = Object.Instantiate(_prefab, _container);
            _pool.Add(createdObject);
            return createdObject;
        }
        #endregion
    }
}
