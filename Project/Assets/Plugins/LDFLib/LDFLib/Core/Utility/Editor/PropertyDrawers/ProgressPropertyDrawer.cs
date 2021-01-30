using LDF.Structures;
using UnityEditor;
using UnityEngine;
using Progress = LDF.Structures.Progress;

namespace LDF.Utility
{
    [PropertyDrawer(typeof(Progress))]
    public class ProgressPropertyDrawer : PropertyDrawer
    {
        private static Progress p;

        private const string _currentPropertyName = "b_current";
        private const string _maxPropertyName = nameof(p.max);

        private const float _dragHandleWidth = 4;

        SerializedProperty _currentProp;
        SerializedProperty _maxProp;

        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            _currentProp = property.FindPropertyRelative(_currentPropertyName);
            _maxProp = property.FindPropertyRelative(_maxPropertyName);
            return true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawFields(GetPropertyRects(ref position), property, label);
        }

        private void DrawFields((Rect current, Rect max, Rect progress) rect, SerializedProperty property, GUIContent label)
        {
            using (var scope = new EditorGUI.ChangeCheckScope())
            {
                float[] values = { _currentProp.floatValue, _maxProp.floatValue};

                EditorGUI.ProgressBar(rect.progress, values[0] / values[1], label.text);
                
                var previousLabelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = _dragHandleWidth;
                
                values[0] = EditorGUI.FloatField(rect.current, "\n", values[0]);
                values[1] = EditorGUI.FloatField(rect.max, "\n", values[1]);
                EditorGUIUtility.labelWidth = previousLabelWidth;

                if (scope.changed)
                {
                    _maxProp.floatValue = Mathf.Clamp(values[1], 0, float.PositiveInfinity);
                    _currentProp.floatValue = Mathf.Clamp(values[0], 0, values[1]);
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
        }

        private static (Rect current, Rect max, Rect progress) GetPropertyRects(ref Rect totalSpace)
        {
            var floatFieldWidth = totalSpace.width / 6;

            return
            (
                current: new Rect
                {
                    x = totalSpace.x,
                    y = totalSpace.y,
                    height = totalSpace.height,
                    width = floatFieldWidth
                },
                max: new Rect
                {
                    x = totalSpace.xMax - floatFieldWidth,
                    y = totalSpace.y,
                    height = totalSpace.height,
                    width = floatFieldWidth
                },
                progress: new Rect
                {
                    x = totalSpace.x + floatFieldWidth,
                    y = totalSpace.y,
                    width = totalSpace.width - 2 * floatFieldWidth,
                    height = totalSpace.height
                }
            );
        }
    }
}