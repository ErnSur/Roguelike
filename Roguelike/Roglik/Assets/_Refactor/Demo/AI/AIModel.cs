using LDF.Roglik;
using UnityEngine;
using LDF.Utility;

namespace LDF.Systems.AI
{
    public class AIModel : MonoBehaviour, IAIMovement_SInput
    {
        [SerializeField]
        private PlayerMovementModel _player;
        public Vector3 Target => _player.PlayerGridPos.ToVector3();
        public Vector3[] Waypoints { get; }
        [field: SerializeField]
        public float Speed { get; private set; }
    }
}