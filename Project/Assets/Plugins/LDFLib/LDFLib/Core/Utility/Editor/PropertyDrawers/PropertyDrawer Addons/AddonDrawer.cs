using System;
using UnityEditor;
using UnityEngine;

namespace LDF.Utility
{
    public abstract class AddonDrawer
    {
        protected Attribute TargetAttribute { get; set; }

        public virtual void Initialize(Attribute attribute) => TargetAttribute = attribute;

        public virtual float GetAddedHeight(SerializedProperty property, GUIContent label) => 0;
        public abstract void BeginAddon(ref Rect position, SerializedProperty property, GUIContent label);
        public abstract void EndAddon(ref Rect position, SerializedProperty property, GUIContent label);
    }
}