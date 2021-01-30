using System;
using System.IO;
using UnityEngine;
using UnityCubemap = UnityEngine.Cubemap;
using UnityObject = UnityEngine.Object;

namespace LDF.Utility
{
    public static class CameraCapture
    {
        const string _pngExtension = "png";

        public static class Cubemap
        {
            private static CubemapFace[] _cubemapFaces =
            {
                CubemapFace.PositiveX,
                CubemapFace.NegativeX,
                CubemapFace.PositiveY,
                CubemapFace.NegativeY,
                CubemapFace.PositiveZ,
                CubemapFace.NegativeZ
            };

            public static CubemapData Capture(Camera _targetCamera, int width)
            {
                Data[] capturedData = new Data[_cubemapFaces.Length];
                UnityCubemap targetCubemap = new UnityCubemap(width, TextureFormat.RGB24, false);
                Texture2D temporaryTexture = new Texture2D(targetCubemap.width, targetCubemap.height, TextureFormat.RGB24, false);

                _targetCamera.RenderToCubemap(targetCubemap);

                for (int i = 0; i < _cubemapFaces.Length; i++)
                {
                    var cubemapSize = targetCubemap.width;
                    var facePixels = targetCubemap.GetPixels(_cubemapFaces[i]);
                    var flippedPixels = facePixels.FlippedVertically(cubemapSize, cubemapSize);

                    temporaryTexture.SetPixels(flippedPixels);
                    temporaryTexture.Apply();

                    capturedData[i] = EncodeToDataPNG(ref temporaryTexture, _cubemapFaces[i].ToString());
                }

                if (Application.isEditor)
                {
                    UnityObject.DestroyImmediate(targetCubemap);
                }
                else
                {
                    UnityObject.Destroy(targetCubemap);
                }

                return new CubemapData { Faces = capturedData };
            }
        }

        public static class Viewport
        {
            public static Data Capture(Camera _targetCamera, Vector2Int resolution)
            {
                if (resolution.x <= 0 || resolution.y <= 0)
                {
                    throw new ArgumentException("Both resolution's coords must be positive (> 0)");
                }

                Texture2D encodableTexture = new Texture2D(resolution.x, resolution.y, TextureFormat.RGB24, false);
                Rect readRect = new Rect(0, 0, resolution.x, resolution.y);

                var temporaryTexture = RenderTexture.GetTemporary(resolution.x, resolution.y);
                var lastTargetTexture = _targetCamera.activeTexture;

                _targetCamera.targetTexture = temporaryTexture;
                _targetCamera.Render();
                _targetCamera.targetTexture = lastTargetTexture;

                var lastActiveTexture = RenderTexture.active;
                RenderTexture.active = temporaryTexture;

                encodableTexture.ReadPixels(readRect, 0, 0);
                encodableTexture.Apply();

                RenderTexture.active = lastActiveTexture;
                RenderTexture.ReleaseTemporary(temporaryTexture);

                return EncodeToDataPNG(ref encodableTexture, GenerateName());

                string GenerateName()
                {
                    return $"{DateTime.Now.ToString("yy-MM-dd-hh-mm-ss")}_{resolution.x}_{resolution.y}";
                }
            }
        }

        public static class Exporter
        {
            public static void SaveToPersistentData(Data payload, string namePrefix = null)
            {
                if(string.IsNullOrWhiteSpace(namePrefix))
                {
                    namePrefix = string.Empty;
                }

                var filename = $"{namePrefix}{payload.Name}";
                var filePath = Path.Combine(Application.persistentDataPath, filename);

                File.WriteAllBytes(filePath, payload.RawData);
            }
        }

        private static Data EncodeToDataPNG(ref Texture2D texture, string name)
        {
            return new Data
            {
                Name = $"{name}.{_pngExtension}",
                RawData = texture.EncodeToPNG()
            };
        }

        public struct Data
        {
            public string Name;
            public byte[] RawData;
        }

        public struct CubemapData
        {
            public Data[] Faces;
        }
    }
}