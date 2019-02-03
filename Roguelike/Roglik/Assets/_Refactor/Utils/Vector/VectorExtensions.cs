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
    }
}