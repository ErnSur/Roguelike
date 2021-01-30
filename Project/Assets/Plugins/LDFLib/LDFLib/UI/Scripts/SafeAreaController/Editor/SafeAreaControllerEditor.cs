#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace LDF.UserInterface.SafeArea
{
    [CustomEditor(typeof(SafeAreaController))]
    public class SafeAreaControllerEditor : Editor
    {
        public SafeAreaEmulatorMode emulationMode;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SafeAreaController controller = target as SafeAreaController;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Safe-Area Test in Editor");

            emulationMode = (SafeAreaEmulatorMode)EditorGUILayout.EnumPopup("Test Device Type:", emulationMode);

            if (!controller.IsSafeAreaOn)
            {
                if (GUILayout.Button("Show Safe-Area"))
                {
                    if (controller)
                    {
                        controller.ShowSafeAreaFrame(emulationMode);
                        controller.SetupDeviceFrame(emulationMode);
                        controller.IsSafeAreaOn = true;
                    }
                }
            }
            else
            {
                if (GUILayout.Button("Hide Safe-Area"))
                {
                    if (controller)
                    {
                        controller.HideSafeAreaFrame();
                        controller.DeleteDeviceFrame();
                        controller.IsSafeAreaOn = false;
                    }
                }
            }

            EditorGUILayout.Space();
        }
    }
}
#endif