using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDF.Utils
{
    public static class VectorExtensions
    {
        public static Vector3 ToVector3(this Vector2Int v)
        {
            return new Vector3(v.x, v.y, 0);
        }
        
        public static Vector2Int ToVector2Int(this Vector3 v)
        {
            return new Vector2Int((int) v.x, (int) v.y);
        }
        
        public static bool IsPositive(this Vector2Int v)
        {
            return v.x >= 0 && v.y >= 0;
        }
    }
}