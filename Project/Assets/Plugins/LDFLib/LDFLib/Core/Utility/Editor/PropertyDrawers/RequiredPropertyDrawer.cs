using System;
using UnityEditor;
using UnityEngine;

namespace LDF.Utility
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredPropertyDrawer : PropertyDrawer
    {
        private bool _componentIsMissing;

        private static readonly Lazy<Texture2D> _errorIcon = new Lazy<Texture2D>(() => GetIcon(_errorIconName));
        private static readonly Lazy<Texture2D> _icon = new Lazy<Texture2D>(() => GetIcon(_iconName));

        private const string _tooltip = "Dependency";
        private const string _errorIconName = "console.erroricon.sml";
        private const string _iconName = "console.erroricon.inactive.sml";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _componentIsMissing = property.objectReferenceValue == null;

            if (_componentIsMissing)
            {
                TryAssignComponent(property);
            }
            else
            {
                ReassignComponentIfPropertyWasCopied(property);
            }

            DrawReadOnlyProperty(position, property, label);
        }

        private void ReassignComponentIfPropertyWasCopied(SerializedProperty property)
        {
            var isReferencingAnotherGameObject =
                                (property.serializedObject.targetObject as MonoBehaviour)?.gameObject !=
                                (property.objectReferenceValue as MonoBehaviour)?.gameObject;

            if (isReferencingAnotherGameObject)
            {
                TryAssignComponent(property);
            }
        }

        private void TryAssignComponent(SerializedProperty property)
        {
            if (RequiredAttribute.TryGetComponent(property.serializedObject.targetObject as MonoBehaviour, fieldInfo.FieldType, out var component))
            {
                property.objectReferenceValue = component;

                property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                Debug.LogError($"Field `{fieldInfo.Name}` on `{property.serializedObject.targetObject.name}` Game Object is Null");
            }
        }

        private void DrawReadOnlyProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            SetLabel(label);

            DrawAddMissingComponentButton(position, property);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.EndDisabledGroup();
        }

        private void SetLabel(GUIContent label)
        {
            label.SetNiceBackingFieldName();
            
            label.image = _componentIsMissing ? _errorIcon.Value : _icon.Value;

            label.tooltip = _tooltip;
        }

        private void DrawAddMissingComponentButton(Rect position, SerializedProperty property)
        {
            if (_componentIsMissing && GUI.Button(position, GUIContent.none, EditorStyles.label))
            {
                AddMissingComponent(property.serializedObject.targetObject as MonoBehaviour);
            }
        }

        private void AddMissingComponent(MonoBehaviour obj)
        {
            obj.gameObject.AddComponent(fieldInfo.FieldType);
        }

        private static Texture2D GetIcon(string iconName) => EditorGUIUtility.IconContent(iconName).image as Texture2D;
    }
}