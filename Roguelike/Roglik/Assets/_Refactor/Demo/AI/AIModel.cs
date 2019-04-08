using LDF.Systems.Pathfinding;
using UnityEngine;

namespace LDF.Systems.AI
{
    public class AIModel : MonoBehaviour, IAIMovement_SInput
    {
        [SerializeField]
        private Transform _player;

        public Vector3 Target => _player.position;
        public Vector3[] Waypoints { get; }
        [field: SerializeField]
        public float Speed { get; private set; }
    }
}