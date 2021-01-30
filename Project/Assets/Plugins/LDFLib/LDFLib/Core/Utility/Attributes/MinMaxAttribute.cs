using System;
using UnityEngine;

namespace LDF.Utility
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class MinMaxAttribute : PropertyAttribute
    {
        public readonly float min;
        public readonly float max;

        public MinMaxAttribute() : this(0, 1) { }

        public MinMaxAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
