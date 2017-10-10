using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State {

    public LayerMask playerAndWall;
    public float noticeRange = 6f;
    public float speed = 2.5f;

    #region ChasePlayer
    void Chase()
    {
        Vector3 playerDir = PlayerMovement.PlayerPos3 - stats.position;
        
        if (transform.position == stats.position && RayPlayerUpdate(playerDir))
        {
            stats.myPath = PFaStar.FindPath(stats.position, PlayerMovement.PlayerPos3);

            if (stats.myPath.Count > 0 && stats.myPath.Count != 1)
            {
                PFnode cell = stats.myPath[0];
                stats.myPath.Remove(cell);

                stats.position = new Vector3(cell.x, cell.y, 0);
                
            }
        }//*
        else if (stats.myPath.Count > 0 && transform.position == stats.position) // if npc loses player from eyes it goes to the place where he saw him last time.
        {
            PFnode cell = stats.myPath[0];
            stats.myPath.Remove(cell);

            stats.position = new Vector3(cell.x, cell.y, 0);
        }//*/
        
    }

    //Raycast FoV
    bool RayPlayerUpdate(Vector3 dir)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, noticeRange, playerAndWall);
        Debug.DrawRay(transform.position, dir, Color.green, 0.1f);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Saw " + hit.collider.name);
            return true;
        }
        return false;
    }
#endregion

    public override void Act()
    {
        Chase();
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
