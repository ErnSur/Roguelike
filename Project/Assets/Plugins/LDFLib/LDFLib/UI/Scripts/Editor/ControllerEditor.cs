using LDF.UserInterface.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Controller),true)]
public class ControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ButtonRow();
        base.OnInspectorGUI();
    }

    private void ButtonRow()
    {
        GUILayout.BeginHorizontal();
        if (Application.isPlaying)
        {
            if (GUILayout.Button("Init"))
            {
                (target as Controller).Initialize();
            }
            if (GUILayout.Button("Show"))
            {
                (target as Controller).ShowView();
            }
            if (GUILayout.Button("Hide"))
            {
                (target as Controller).HideView();
            }
        }
        GUILayout.EndHorizontal();
    }
}
