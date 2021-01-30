using UnityEngine;

namespace LDF.Utility
{
    public static class QuaterionExtensions
    {
        public static Quaternion ZeroAxisZ(this Quaternion q)
        {
            q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            return q;
        }

        /// <summary>
        /// Clamps quaternion around the axis x.
        /// </summary>
        /// <returns>The axis x.</returns>
        /// <param name="q">Q.</param>
        /// <param name="min">Minimum angle in radians.</param>
        /// <param name="max">Max angle in radians.</param>
        public static Quaternion ClampAxisX(this ref Quaternion q, float min, float max)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleXRad = 2.0f * Mathf.Atan(q.x);
            angleXRad = Mathf.Clamp(angleXRad, min, max);
            q.x = Mathf.Tan(0.5f * angleXRad);

            return q;
        }
    }
}