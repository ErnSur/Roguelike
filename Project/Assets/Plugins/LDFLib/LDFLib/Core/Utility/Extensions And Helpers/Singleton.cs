using UnityEngine;

namespace LDF.Core
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static bool _isAppQutting;

        private static T _instance;
        public static T Instance => GetInstance();

        private static T GetInstance()
        {
            if (_isAppQutting)
            {
                return null;
            }

            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    var obj = new GameObject { name = typeof(T).Name };
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if(_instance == this)
            {
                _instance = null;
                _isAppQutting = true;
            }
        }
    }
}