using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Stone), true, isFallback = true)]
public class CustomStoneEditor : Editor
{
    private Stone _stone;

    private void OnEnable()
    {
        _stone = target as Stone;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DisplayAttackPattern(16, 2);

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayAttackPattern(float cellSize, float padding)
    {
        var cellTrueSize = cellSize + (2 * padding);

        var numberOfRows = _stone.pattern.Height;
        var numberOfColumns = _stone.pattern.Width;

        var rectHeight = numberOfRows * cellTrueSize;
        var rectWidth = numberOfColumns * cellTrueSize;
        
        GUILayout.BeginVertical(GUILayout.Width(rectWidth), GUILayout.Height(rectHeight));

        for (int y = 0; y < _stone.pattern.Height; y++)
        {
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true), GUILayout.Height(cellTrueSize));
            
            for (int x = 0; x < _stone.pattern.Width; x++)
            {
                var propRect = GUILayoutUtility.GetRect(cellTrueSize, cellTrueSize);

                if(_stone.PlayerOnPatternGrid == new Vector2Int(x,y))
                    continue;
                
                var prop = GetProp(x, y);
                EditorGUI.DrawRect(propRect, prop.boolValue ? new Color(0.8f, 0.3f, 0.2f) : Color.white);

                EditorGUI.PropertyField(propRect, prop, GUIContent.none);
            }

            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    private void DrawProp(int x, int y)
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            var prop = GetProp(x, y);
            
            var newValue = GUILayout.Toggle(prop.boolValue, GUIContent.none);
            
            if (check.changed)
                prop.boolValue = newValue;
        }
    }

    private SerializedProperty GetProp(int x, int y)
    {
        var propPath = $"{nameof(_stone.pattern)}.array.Array.data[{x + y * _stone.pattern.Width}]";
        return serializedObject.FindProperty(propPath);
    }
}