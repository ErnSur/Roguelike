using System;
using UnityEngine;

namespace LDF.Utility
{
    public static class MonoBehaviourExtensions
    {
        public static TComponent Lazy<TBehaviour,TComponent>(this TBehaviour obj, ref TComponent field)
        where TComponent : Component
        where TBehaviour : MonoBehaviour
        {
            if (field != null)
            {
                return field;
            }

            if(obj == null)
            {
                throw new ArgumentNullException(nameof(obj),$"{typeof(TBehaviour).Name} component is NULL");
            }

            field = obj.GetComponent<TComponent>();
            
            if (field == null)
            {
                throw new NullReferenceException($"There is no {typeof(TComponent).Name} component on {obj.name} game object.");
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