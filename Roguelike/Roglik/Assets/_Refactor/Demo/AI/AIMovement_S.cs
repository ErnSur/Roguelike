
using System.Linq;
using UnityEngine;

namespace LDF.Systems.AI
{
    public interface IAIMovement_SInput
    {
        Vector3 Target { get; }
        Vector3[] Waypoints { get; }
        float Speed { get; }
    }
    
    public class AIMovement_S : TurnSystemBehaviour<IAIMovement_SInput>, IMovable
    {
        private Vector3[] _currentPath;

        private Vector3 _nextPosition;

        protected override void OnTurnStart(Turn turn)
        {
            if(turn == Turn.Player)
                return;
            
            UpdatePathTo(input.Target);
            _nextPosition = _currentPath[0];
        }

        protected override void OnTurnUpdate(Turn turn)
        {
            if(turn == Turn.Player)
                return;
            
            TakeAction();
        }

        private void TakeAction()
        {
            if (MoveInDirection(transform, _nextPosition, input.Speed))
            {
                EndTurn($"{name}: Im going for you Knight!");
            }
        }

        private void ChooseAPatrol()
        {
            var waypoint = GetRandomWaypoint();

            UpdatePathTo(waypoint);
        }

        private void UpdatePathTo(Vector3 waypoint)
        {
            _currentPath = ScenePathfinding.GetPositionPath(transform.position, waypoint).ToArray();
        }

        private Vector3 GetRandomWaypoint()
        {
            return input.Waypoints[Random.Range(0, input.Waypoints.Length - 1)];
        }
        
        public bool MoveInDirection(Transform subject, Vector3 destination, float speed)
        {
            return this.DefaultImplementation(subject, destination, speed);
        }
    }
}