using System.IO;
using UnityEditor;
using UnityEngine;

namespace LDF.Utility
{
    public class ScreenCaptureWindow : EditorWindow
    {
        private string _baseName = "Capture";
        private Mode _mode = Mode.Cubemap;
        private int _targetWidth = 2048;

        private Camera _targetCamera;

        [MenuItem("Varunit/Utility/Show ScreenCapture")]
        public static void Initialize()
        {
            var instance = CreateInstance<ScreenCaptureWindow>();
            instance.Show();
        }

        private void OnGUI()
        {
            DrawHeader();
            DrawContent();
        }

        private void DrawHeader()
        {
            GUILayout.Label("ScreenCapture", EditorStyles.boldLabel);
        }

        private void DrawContent()
        {
            _mode = (Mode)EditorGUILayout.EnumPopup("Current mode", _mode);
            if (_mode != Mode.Cubemap)
            {
                return;
            }

            switch (_mode)
            {
                case Mode.Cubemap:
                    {
                        DrawCubemapContent();
                    }
                    break;
                default:
                    {
                        EditorGUILayout.HelpBox("Currently only Cubemap mode is available", MessageType.Info);
                    }
                    break;
            }
        }

        private void DrawCubemapContent()
        {
            if (!DrawAndValidateCameraSelection())
            {
                EditorGUILayout.HelpBox("To take screen capture you must select target camera from scene (not a prefab!)", MessageType.Info);
                return;
            }

            DrawCaptureSettings();
            DrawCubemapSaveButton();
        }

        private void DrawCaptureSettings()
        {
            _targetWidth = EditorGUILayout.IntField("Cubemap width", _targetWidth);
            if (_targetWidth <= 0)
            {
                _targetWidth = 1;
            }

            _baseName = EditorGUILayout.TextField("Cubemap name", _baseName);
        }

        private bool DrawAndValidateCameraSelection()
        {
            _targetCamera = (Camera)EditorGUILayout.ObjectField("Camera for screen capture", _targetCamera, typeof(Camera), true);
            return _targetCamera;
        }

        private void DrawCubemapSaveButton()
        {
            if (GUILayout.Button("Save"))
            {
                var folder = EditorUtility.SaveFolderPanel("Save files to folder", "", "ScreenCapture");
                if (folder.Length == 0)
                {
                    return;
                }
                
                OnCubemapSaveButtonClicked(folder);
            }
        }

        private void OnCubemapSaveButtonClicked(string folderPath)
        {
            var capturedCubemap = CameraCapture.Cubemap.Capture(_targetCamera, _targetWidth);
            foreach (var face in capturedCubemap.Faces)
            {
                SaveToFile(folderPath, $"{_baseName}-{face.Name}", ".png", face.RawData);
            }
            Debug.Log($"Saved files under: {folderPath}");
        }

        private void SaveToFile(string folderPath, string fileName, string extension, byte[] data)
        {
            var filePath = Path.Combine(folderPath, $"{fileName}.{extension}");
            File.WriteAllBytes(filePath, data);
        }

        private enum Mode
        {
            Viewport = 0,
            Cubemap = 10
        }
    }
}