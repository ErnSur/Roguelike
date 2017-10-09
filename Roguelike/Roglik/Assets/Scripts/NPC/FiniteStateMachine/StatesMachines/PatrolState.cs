using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State {

    public StateController controller;

    public Transform[] patrolWaypoints;
    public Transform randomWaypoint;
    public float speed = 2.5f;
    public NPCStats stats;
    //private List<PFnode> myPath;

    public Trigger trigger;


    public void FindPatrol()
    {
        randomWaypoint = patrolWaypoints[Random.Range(0, patrolWaypoints.Length - 1)];
    }

    public void Patrol()
    {
        if (transform.position == randomWaypoint.position)
        {
            Debug.Log("done patroling");
            FindPatrol();
        }

        stats.position = transform.position;

        if (transform.position == stats.position)
        {
            stats.myPath = PFaStar.FindPath(transform, randomWaypoint);

            if (stats.myPath.Count > 0)
            {
                PFnode cell = stats.myPath[0];
                stats.myPath.Remove(cell);

                stats.position = new Vector3(cell.x, cell.y, 0);

            }
        }
    }

    public override void Act()
    {
        Patrol();
    }

    void Start () {
        //Debug.Log("find Patrol");
        FindPatrol();
    }

    private void Update() //Change of state
    {
        transform.position = Vector3.MoveTowards(transform.position, stats.position, Time.deltaTime * speed);

        State newState = trigger.TriggerEvent(stats.position, PlayerMovement.PlayerPos3);
        if (newState != null && transform.position == stats.position)
        {
            controller.currentState = newState;
            this.enabled = false;
        }
    }
}
