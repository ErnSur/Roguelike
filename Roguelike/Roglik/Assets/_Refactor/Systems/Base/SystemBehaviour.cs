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

        protected bool GetComponent<T>(out T field)
        {
            field = GetComponent<T>();
            return field != null;
        }
    }

    public abstract class SystemBehaviour<T> : SystemBehaviour
    {
        protected T input;

        protected override void Awake()
        {
            GetSystemInput();
            Init();
        }

        private void GetSystemInput()
        {
            if (GetComponent(out input))
            {
                throw new NullReferenceException(
                    $"{name} was not Initialized, couldn't find input of type {typeof(T).Name}");
            }
        }
    }
}