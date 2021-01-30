using System.Linq;
using LDF.Systems;
using UnityEngine;
using LDF.Utility;

namespace LDF.Roglik
{
    public class PlayerMovementModel : MonoBehaviour, IPlayerMovement_SInput
    {
        [field: SerializeField, ReadOnly]
        public Vector2Int PlayerGridPos { get; set; }

        [field: SerializeField]
        public float PlayerSpeed { get; private set; } = 2;

        public Transform PlayerTransform => transform;

        private void Awake()
        {
            PlayerGridPos = transform.GridPosition();
        }

#if DEBUG
        private void OnValidate()
        {
            PlayerGridPos = PlayerTransform.GridPosition();
        }
#endif
    }
}