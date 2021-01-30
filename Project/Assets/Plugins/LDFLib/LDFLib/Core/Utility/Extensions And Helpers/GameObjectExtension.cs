using UnityEngine;

namespace LDF.Utility
{
    public static class GameObjectExtension
    {
        public static T GetOrAddComponent<T>(this GameObject target) where T : Component
        {
            var component = target.GetComponent<T>();
            if (component == null)
            {
                component = target.AddComponent<T>();
            }

            return component;
        }

        public static void SetLayerToChildrens(this GameObject target, int newLayer, bool includeParent = true)
        {
            var childs = target.GetComponentsInChildren<Transform>();
            for (int i = includeParent ? 0 : 1; i < childs.Length; ++i)
            {
                childs[i].gameObject.layer = newLayer;
            }
        }
    }
}