using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace LDF.Utility
{
    [CustomPropertyDrawer(typeof(ExtendedListAttribute))]
    public class NamedListEementPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue != null)
            {
                var field = property.objectReferenceValue
                                    .GetType()
                                    .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                    .FirstOrDefault(prop => prop.IsDefined(typeof(ExtendedListAttribute.ElementNameAttribute), true));

                if (field != null)
                {
                    label.text = field.GetValue(property.objectReferenceValue).ToString();
                }
            }

            EditorGUI.PropertyField(position, property, label);
        }
    }
}
