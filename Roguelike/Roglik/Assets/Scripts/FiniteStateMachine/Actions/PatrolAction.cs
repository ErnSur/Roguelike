using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="AI/Actions/Patrol")]
public class PatrolAction : Action {

    public Transform[] patrolWaypoints;

    public override void Act(StateController controller)
    {

        throw new System.NotImplementedException();
    }

    private void Patrol(StateController controller)
    {
        Transform randomWaypoint = patrolWaypoints[Random.Range(0, patrolWaypoints.Length - 1)];

        controller.pathfinding.FindPath(controller.gameObject.transform,randomWaypoint);


    }
}
