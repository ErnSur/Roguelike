using System;
using UnityEngine;

namespace LDF.Systems
{
    public abstract class SystemBehaviour : MonoBehaviour
    {
        protected virtual void Awake() => Init();

        protected virtual void Init()
        {
        }

        protected T GetComponent<T>(ref T field) => field = GetComponent<T>();
    }

    public abstract class SystemBehaviour<T> : SystemBehaviour
    {
        protected T input { get; private set; }

        protected override void Awake()
        {
            GetSystemInput();
            Init();
        }

        private void GetSystemInput()
        {
            input = GetComponent<T>();

            if (input == null)
            {
                throw new NullReferenceException(
                    $"{name} was not Initialized, couldn't find input of type {typeof(T).Name}");
            }
        }
    }

    public static class MonoBehaviourExtensions
    {
        public static T Lazy<T>(this MonoBehaviour obj, ref T field) where T : Component
        {
            if (field != null)
            {
                return field;
            }

            field = obj.GetComponent<T>();
            
            if (field == null)
            {
                throw new NullReferenceException($"There is no {typeof(T).Name} component on {obj.name} game object.");
            }
            
            return field;
        }
    }
}