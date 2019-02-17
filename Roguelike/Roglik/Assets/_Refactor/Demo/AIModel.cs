using System.Collections.Generic;
using LDF.Systems.Pathfinding;
using UnityEngine;

namespace LDF.Systems.AI
{
    public class AIModel : MonoBehaviour
    {
        
    }

    public interface IAIMovement_SInput
    {
        Vector3[] Waypoints { get; }
        float Speed { get; }
        
    }
    
    public class AIMovement_S : TurnSystemBehaviour<IAIMovement_SInput>
    {
        private IEnumerable<Vector3> _currentPath;

        protected override void OnTurnUpdate(Turn turn)
        {
            if(turn == Turn.Player)
                return;
            TakeAction();
        }

        private void TakeAction()
        {
            
        }


        private void ChooseAPatrol()
        {
            var waypoint = GetRandomWaypoint();

            UpdatePathTo(waypoint);
        }

        private void UpdatePathTo(Vector3 waypoint)
        {
            _currentPath = ScenePathfinding.GetPositionPath(transform.position, waypoint);
        }

        private Vector3 GetRandomWaypoint()
        {
            return input.Waypoints[Random.Range(0, input.Waypoints.Length - 1)];
        }
    }
}