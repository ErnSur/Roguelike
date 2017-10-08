using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

    public PFaStar pathfinding;
    public float speed = 3.5f;
    public float noticeRange = 6f; // 1f = One cell

    public Transform player;
    public LayerMask playerLayer;

    private Vector3 pos;
    public List<PFnode> myPath;


    void ChasePlayer()
    {
        Vector3 playerDir = player.transform.position - transform.position;

        if(transform.position == pos && RayPlayerUpdate(playerDir))
        {
            /*List<PFnode>*/ myPath = PFaStar.FindPath(transform, player);

            if (myPath.Count > 0 && myPath.Count != 1)
            {
                PFnode cell = myPath[0];
                myPath.Remove(cell);
                
                pos = new Vector3(cell.x,cell.y,0);
            }
        }
        else if (transform.position == pos) // if npc loses player from eyes it goes to the place where he saw him last time.
        {
            if(myPath.Count > 0)
            {
                PFnode cell = myPath[0];
                myPath.Remove(cell);

                pos = new Vector3(cell.x, cell.y, 0);
            }
        }

        //transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    void Patrol()
    {

    }

    //Raycast FoV
    bool RayPlayerUpdate(Vector3 dir)
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, noticeRange, playerLayer);
        Debug.DrawRay(transform.position, dir, Color.green, 0.1f);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("hit " + hit.collider.name);
            return true;
        }
        return false;
    }

    void Update () {
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);

    }

    void Start () {
        pos = transform.position;
        TurnSystem.enemyTurn += ChasePlayer;
    }
}
