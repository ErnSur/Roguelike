using UnityEditor;
using UnityEngine;

namespace LDF.Utility
{
    // ReflectionTarget
    // ReSharper disable once UnusedMember.Global
    [CustomAddonDrawerAttribute(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAddonDrawer : AddonDrawer
    {
        public override void BeginAddon(ref Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginDisabledGroup(true);
        }

        public override void EndAddon(ref Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.EndDisabledGroup();
        }
    }
}