#if DEBUG
using System.Collections.Generic;
using UnityEngine;

namespace LDF.Helpers
{
    public static partial class RDebug
    {
        public static bool IsDebugModeOn { get; private set; }

        static RDebug()
        {
            InitStyles();
        }

        public static void OnGUI(params object[] content)
        {
            DebugModeToggle();

            if (!IsDebugModeOn)
            {
                return;
            }

            ConsoleOnGUI();
            LabelsOnGUI(content);
        }

        private static void DebugModeToggle()
        {
            using (var horizontalScope = new GUILayout.HorizontalScope("box"))
            {
                IsDebugModeOn = GUILayout.Toggle(IsDebugModeOn, $"{(IsDebugModeOn ? "Hide" : "Show")} Debug View", _labelStyle);
            }
        }
    }

    //Labels
    public static partial class RDebug
    {
        private static void LabelsOnGUI(object[] content)
        {
            using (var verticalScope = new GUILayout.VerticalScope("box"))
            {
                foreach (var item in content)
                {
                    GUILayout.Label(item.ToString(), _labelStyle);
                }
            }
        }
    }

    /// #Console
    public static partial class RDebug
    {
        private const int _consoleLogCapacity = 200;
        private static List<ConsoleLog> _consoleLogs = new List<ConsoleLog>(_consoleLogCapacity);
        private static Vector2 _consolePosition;

        private static void ConsoleOnGUI()
        {
            using (var scrollScope = new GUILayout.ScrollViewScope(_consolePosition, "box", GUILayout.Height(500)))
            {
                _consolePosition = scrollScope.scrollPosition;

                for (int i = 0; i < _consoleLogs.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(_consoleLogs[i].Msg, _labelStyle);
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(_consoleLogs[i].Count.ToString(), _labelStyle);
                    GUILayout.EndHorizontal();
                }
            }
        }

        public static void Log(object msg)
        {
            var index = _consoleLogs.Count - 1;
            if (index > -1 && _consoleLogs[index]?.Msg == msg.ToString())
            {
                _consoleLogs[index].Count++;
                return;
            }

            if (_consoleLogs.Count == _consoleLogCapacity)
            {
                _consoleLogs.RemoveAt(0);
            }

            _consoleLogs.Add(new ConsoleLog { Msg = msg.ToString() });
        }

        public class ConsoleLog { public string Msg; public int Count; }
    }

    /// #Rays
    public static partial class RDebug
    {
        public static void DrawRay(Vector3 start, Vector3 destination)
        {
            Debug.DrawRay(start, destination - start);
        }

        public static void DrawRay(Vector3 start, Vector3 destination, Color color)
        {
            Debug.DrawRay(start, destination - start, color);
        }

        public static void DrawRay(Vector3 start, Vector3 destination, Color color, float duration)
        {
            Debug.DrawRay(start, destination - start, color, duration);
        }
    }

    //Gizmos
    public static partial class RDebug
    {
        public static void DrawSphereAt(Vector3 point, float radius = 1)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(point, radius);
            Gizmos.color = Color.white;
        }
    }

    //Style
    public static partial class RDebug
    {
        private static float _fontSize = 30;

        private static GUIStyle _labelStyle;

        private static void InitStyles()
        {
            _labelStyle = CreateLabelStyle();
        }

        private static GUIStyle CreateLabelStyle()
        {
            var style = new GUIStyle("label");

            style.fontSize = Mathf.Min(Mathf.FloorToInt(Screen.width * _fontSize / 1000), Mathf.FloorToInt(Screen.height * _fontSize / 1000));
            style.normal.textColor = Color.white;

            return style;
        }
    }
}
#endif