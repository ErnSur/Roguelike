using UnityEngine;

namespace LDF.Structures
{
    [System.Serializable]
    public class Progress
    {
        [SerializeField]
        private float b_current;

        public float Current
        {
            get => b_current;
            set => b_current = Mathf.Clamp(value, 0, max);
        }
        
        public float max;

        public float Percentage => b_current / max;

        public Progress(float current, float max)
        {
            this.b_current =  Mathf.Clamp(current, 0, max);
            this.max = max;
            
            Current = current;
        }

        public Progress(float max)
        {
            this.b_current = 0;
            this.max = max;
        }

        public static implicit operator Vector2(Progress p) => new Vector2(p.Current, p.max);

        public static implicit operator Progress(Vector2 v) => new Progress(v.x, v.y);
    }
}