using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="AI/States/Patrol")]
public class ScrObjPatrolState : State {

    public Transform[] patrolWaypoints;
    public Transform randomWaypoint;
    public float speed = 2.5f;

    private List<PFnode> myPath;
    private Vector3 pos;
    private Transform thisTransform;



    public void FindPatrol()
    {
        randomWaypoint = patrolWaypoints[Random.Range(0, patrolWaypoints.Length - 1)];
    }

    public void Patrol()
    {
        if(randomWaypoint == null || thisTransform == randomWaypoint)
        {
            Debug.Log("find Patrol");
            FindPatrol();
        }

        thisTransform = transform;
        pos = thisTransform.position;

        if (thisTransform.position == pos)
        {
            myPath = PFaStar.FindPath(thisTransform, randomWaypoint);

            if (myPath.Count > 0)
            {
                PFnode cell = myPath[0];
                myPath.Remove(cell);

                pos = new Vector3(cell.x, cell.y, 0);

            }
        }
    }

    public override void Act()
    {
        Patrol();
    }

}
