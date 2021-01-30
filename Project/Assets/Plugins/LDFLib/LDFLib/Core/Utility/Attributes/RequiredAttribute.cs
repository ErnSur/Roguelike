using System;
using System.Linq;
using UnityEngine;

namespace LDF.Utility
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredAttribute : PropertyAttribute
    {
#if UNITY_EDITOR
        public static void CollectDependencies<T>(T obj) where T : MonoBehaviour
        {
            var fields = obj.GetType().GetFields()
                    .Where(field => field.IsDefined(typeof(RequiredAttribute), false));

            foreach (var field in fields)
            {
                field.SetValue(obj, obj.GetComponent(field.FieldType));
            }
        }

        public static bool TryGetComponent(MonoBehaviour obj, Type componentType, out UnityEngine.Object component)
        {
            if(obj == null)
            {
                component = null;
            }
            else
            {
                component = obj.GetComponent(componentType);
            }

            return component != null;
        }
#endif
    }
}