using UnityEngine;

namespace LDF.Utility
{
    public class ViewportExporter : MonoBehaviour
    {
        [SerializeField]
        private string _filePrefix;

        [SerializeField]
        private Vector2Int _resolution;

        [SerializeField, Required]
        private Camera _targetCamera;

        [ContextMenu("Save viewport")]
        public void SaveViewport()
        {
            var capturedViewport = CameraCapture.Viewport.Capture(_targetCamera, _resolution);
            CameraCapture.Exporter.SaveToPersistentData(capturedViewport, _filePrefix);
        }
    }
}