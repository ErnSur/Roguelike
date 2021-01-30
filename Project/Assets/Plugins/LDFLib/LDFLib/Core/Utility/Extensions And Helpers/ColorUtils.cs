using System.Runtime.CompilerServices;
using UnityEngine;

namespace LDF.Core
{
    public static class ColorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color32 Combine(Color32 lhs, Color32 rhs)
        {
            var r = (byte)((lhs.r + rhs.r) >> 1);
            var g = (byte)((lhs.g + rhs.g) >> 1);
            var b = (byte)((lhs.b + rhs.b) >> 1);
            var a = (byte)((lhs.a + rhs.a) >> 1);

            return new Color32(r, g, b, a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Combine(Color lhs, Color rhs)
        {
            var r = (lhs.r + rhs.r) / 2f;
            var g = (lhs.g + rhs.g) / 2f;
            var b = (lhs.b + rhs.b) / 2f;
            var a = (lhs.a + rhs.a) / 2f;

            return new Color(r, g, b, a);
        }
    }
}