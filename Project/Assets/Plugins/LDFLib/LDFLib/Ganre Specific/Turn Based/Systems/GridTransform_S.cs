using LDF.Utility;
using UnityEngine;

namespace LDF.Systems
{
    //this component might be useless
    [ExecuteInEditMode]
    public class GridTransform_S : MonoBehaviour
    {
        [field: SerializeField]
        public Vector2Int Position { get; set; }
        
#if UNITY_EDITOR
        private void Update()
        {
            if (!Application.isPlaying)
            {
                Position = transform.position.ToVector2Int();
            }
        }
#endif
    }
    //do this instead
    public static class TransformExtensions
    {
        public static Vector2Int GridPosition(this Transform t)
        {
            return t.position.ToVector2Int();
        }
    }
    
    
}