using UnityEngine;

namespace LDF.Utility
{
    public static class VectorExtensions
    {
        #region Conversion
        public static Vector3 ToVector3(this Vector2Int v)
        {
            return new Vector3(v.x, v.y, 0);
        }

        public static Vector2Int ToVector2Int(this Vector3 v)
        {
            return new Vector2Int((int)v.x, (int)v.y);
        }

        public static Vector2 ToVector2_XZ(this Vector3 lhs)
        {
            return new Vector2(lhs.x, lhs.z);
        }
        #endregion
        public static bool IsPositive(this Vector2Int v)
        {
            return v.x >= 0 && v.y >= 0;
        }

        public static Vector3 ClampVector(Vector3 value, Vector3 min, Vector3 max)
        {
            return new Vector3
            {
                x = Mathf.Clamp(value.x, min.x, max.x),
                y = Mathf.Clamp(value.y, min.y, max.y),
                z = Mathf.Clamp(value.z, min.z, max.z)
            };
        }

        public static Vector2 ClampVector(Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2
            {
                x = Mathf.Clamp(value.x, min.x, max.x),
                y = Mathf.Clamp(value.y, min.y, max.y)
            };
        }
    }
}