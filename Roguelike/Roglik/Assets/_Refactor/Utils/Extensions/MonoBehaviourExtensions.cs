using System;
using UnityEngine;

namespace LDF.Systems
{
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
        
        public static bool GetComponent<T>(this MonoBehaviour obj, out T field)
        {
            field = obj.GetComponent<T>();
            return field != null;
        }
    }
}