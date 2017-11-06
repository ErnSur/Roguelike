using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State {

    public LayerMask playerAndWall;
    public float noticeRange = 6f;
    public float speed = 2.5f;

    #region ChasePlayer
    bool Chase()
    {
        Vector3 playerDir = PlayerStats.instance.previousPosition - stats.Position;

        if (transform.position == stats.Position && RayPlayerUpdate(playerDir)) //If NPC Saw player on his old position
        {
            stats.myPath = PFaStar.FindPath(stats.Position, PlayerStats.instance.Position); //Go to the position that he went to

            if (stats.myPath.Count > 0 && stats.myPath.Count != 1) //if it is 1 it means npc stands next to player // error when has a path but something is blocking it
            {
				Debug.Log("hehe");
                PFnode cell = stats.myPath[0];
                stats.myPath.Remove(cell);

                stats.Position = new Vector3(cell.x, cell.y, 0); //move

            }
        }//*
        else if (stats.myPath.Count > 0 && transform.position == stats.Position && stats.myPath[0].walkable) // if npc loses player from eyes it goes to the place where he saw him last time.
        {
            PFnode cell = stats.myPath[0];
            stats.myPath.Remove(cell);

            stats.Position = new Vector3(cell.x, cell.y, 0);
        }else if (!stats.myPath[0].walkable)
		{
			return true;
		}

		if(stats.myPath.Count == 0 )
		{
			return true;
		}else
		{
			return false;
		}
    }

    //Raycast FoV
    bool RayPlayerUpdate(Vector3 dir)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, noticeRange, playerAndWall);
        Debug.DrawRay(transform.position, dir, Color.green, 0.1f);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            //Debug.Log("Saw " + hit.collider.name);
            return true;
        }
        return false;
    }
#endregion

    public override void Act()
    {
        isStateDone = Chase();
        StartCoroutine("MoveToPosition");
        UpdateState();
    }

    IEnumerator MoveToPosition()
    {
        while (transform.position != stats.Position)
        {
            transform.position = Vector3.MoveTowards(transform.position, stats.Position, Time.deltaTime * speed);
            yield return null;
        }
    }
}
