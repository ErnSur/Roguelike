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
}