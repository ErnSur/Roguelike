using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.XR;
using Object = UnityEngine.Object;

namespace LDF.Utility
{
    [CustomPropertyDrawer(typeof(bool), true)]
    [CustomPropertyDrawer(typeof(int), true)]
    [CustomPropertyDrawer(typeof(uint), true)]
    [CustomPropertyDrawer(typeof(float))]
    [CustomPropertyDrawer(typeof(Vector2), true)]
    [CustomPropertyDrawer(typeof(Vector3), true)]
    [CustomPropertyDrawer(typeof(Vector2Int), true)]
    [CustomPropertyDrawer(typeof(Vector3Int), true)]
    [CustomPropertyDrawer(typeof(Rect), true)]
    [CustomPropertyDrawer(typeof(string), true)]
    [CustomPropertyDrawer(typeof(object), true)]
    [CustomPropertyDrawer(typeof(Object), true)]
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute), true)]
    public class MasterPropertyDrawer : PropertyDrawer
    {
        private readonly Lazy<List<AddonDrawer>> _matchingAddonDrawers;
        private readonly Lazy<PropertyDrawer> _matchingPropertyDrawer;

        public MasterPropertyDrawer()
        {
            _matchingAddonDrawers =
                new Lazy<List<AddonDrawer>>(()=>PropertyDrawerFactory.GetAddons(fieldInfo));
            _matchingPropertyDrawer =
                new Lazy<PropertyDrawer>(() => PropertyDrawerFactory.GetPropertyDrawer(fieldInfo));
        }

        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            if (_matchingPropertyDrawer.Value != null)
            {
                return _matchingPropertyDrawer.Value.CanCacheInspectorGUI(property);
            }

            return true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_matchingPropertyDrawer.Value != null)
            {
                return _matchingPropertyDrawer.Value.GetPropertyHeight(property, label) +
                       GetAddonsHeight(property, label);
            }

            return EditorGUI.GetPropertyHeight(property, label, true) + GetAddonsHeight(property, label);
        }

        protected float GetAddonsHeight(SerializedProperty property, GUIContent label)
        {
            return _matchingAddonDrawers.Value.Select(d => d.GetAddedHeight(property, label)).Sum();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.SetNiceBackingFieldName();

            _matchingAddonDrawers.Value.ForEach(d => d.BeginAddon(ref position, property, label));

            OnGUIWithAddons(position, property, label);

            _matchingAddonDrawers.Value.ForEach(d => d.EndAddon(ref position, property, label));
        }

        public virtual void OnGUIWithAddons(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_matchingPropertyDrawer.Value == null)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
            else
            {
                _matchingPropertyDrawer.Value.OnGUI(position, property, label);
            }
        }
    }
}