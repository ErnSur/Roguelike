using UnityEngine;

namespace LDF.Utility
{
    public class CubemapExporter : MonoBehaviour
    {
        [SerializeField]
        private string _filePrefix;

        [SerializeField]
        private int _cubemapWidth;

        [SerializeField, Required]
        private Camera _targetCamera;

        [ContextMenu("Save 360°")]
        public void SaveCubemap()
        {
            var capturedCubemap = CameraCapture.Cubemap.Capture(_targetCamera, _cubemapWidth);

            var faces = capturedCubemap.Faces;
            for (int i = 0; i < faces.Length; ++i)
            {
                CameraCapture.Exporter.SaveToPersistentData(faces[i], _filePrefix);
            }
        }
    }
}