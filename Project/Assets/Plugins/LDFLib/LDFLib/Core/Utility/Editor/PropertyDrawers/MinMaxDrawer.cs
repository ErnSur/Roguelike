﻿using UnityEditor;
using UnityEngine;

namespace LDF.Utility
{
    [PropertyDrawer(typeof(MinMaxAttribute))]
    public class MinMaxDrawer : PropertyDrawer
    {
        private const string kVectorMinName = "x";
        private const string kVectorMaxName = "y";
        private const float kFloatFieldWidth = 30f;
        private const float kSpacing = 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                var spacing = kSpacing * EditorGUIUtility.pixelsPerPoint;

                Vector2 range = property.vector2Value;
                var min = range.x;
                var max = range.y;

                var attr = attribute as MinMaxAttribute;

                EditorGUI.PrefixLabel(position, label);

                Rect sliderPos = position;
                sliderPos.x += EditorGUIUtility.labelWidth + kFloatFieldWidth + spacing;
                sliderPos.width -= EditorGUIUtility.labelWidth + (kFloatFieldWidth + spacing) * 2;

                EditorGUI.BeginChangeCheck();
                EditorGUI.MinMaxSlider(sliderPos, ref min, ref max, attr.min, attr.max);
                if (EditorGUI.EndChangeCheck())
                {
                    range.x = min;
                    range.y = max;
                    property.vector2Value = range;
                }

                Rect minPos = position;
                minPos.x += EditorGUIUtility.labelWidth;
                minPos.width = kFloatFieldWidth;

                EditorGUI.showMixedValue = property.FindPropertyRelative(kVectorMinName).hasMultipleDifferentValues;
                EditorGUI.BeginChangeCheck();
                min = EditorGUI.FloatField(minPos, min);
                if (EditorGUI.EndChangeCheck())
                {
                    range.x = Mathf.Max(min, attr.min);
                    property.vector2Value = range;
                }

                Rect maxPos = position;
                maxPos.x += maxPos.width - kFloatFieldWidth;
                maxPos.width = kFloatFieldWidth;

                EditorGUI.showMixedValue = property.FindPropertyRelative(kVectorMaxName).hasMultipleDifferentValues;
                EditorGUI.BeginChangeCheck();
                max = EditorGUI.FloatField(maxPos, max);
                if (EditorGUI.EndChangeCheck())
                {
                    range.y = Mathf.Min(max, attr.max);
                    property.vector2Value = range;
                }

                EditorGUI.showMixedValue = false;
            }
            else
            {
                EditorGUI.LabelField(position, label, new GUIContent("Vector2 support only"));
            }
        }
    }
}