using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State {

    public Transform[] patrolWaypoints;
    public float speed = 2.5f;

    private Transform randomWaypoint;

    void Start () {
        FindPatrol();
    }
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

        //stats.position = transform.position;

        if (transform.position == stats.position)
        {
            stats.myPath = PFaStar.FindPath(stats.position, randomWaypoint.position);

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
        StartCoroutine("MoveToPosition");
        UpdateState();
    }

    IEnumerator MoveToPosition()
    {
        while (transform.position != stats.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, stats.position, Time.deltaTime * speed);
            yield return null;
        }
    }
}
