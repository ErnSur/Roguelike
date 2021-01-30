using System.Collections;
using UnityEngine;

public class ChaseState : State
{
    public LayerMask playerAndWall;
    public float noticeRange = 6f;
    public float speed = 2.5f;

    private bool Chase()
    {
        Vector3 playerDir = PlayerStats.instance.previousPosition - stats.Position;

        if (transform.position == stats.Position && RayPlayerUpdate(playerDir)) //If NPC Saw player on his old position
        {
            stats.myPath = PFaStar.FindPath(stats.Position, PlayerStats.instance.Position); //Go to the position that he went to

            if (stats.myPath.Count > 0 && stats.myPath.Count != 1) //if it is 1 it means npc stands next to player // error when has a path but something is blocking it
            {
                PFNode cell = stats.myPath[0];
                stats.myPath.Remove(cell);

                stats.Position = new Vector3(cell.x, cell.y, 0); //move
            }
        }
        else if (stats.myPath.Count > 0 && transform.position == stats.Position && stats.myPath[0].Walkable) // if npc loses player from eyes it goes to the place where he saw him last time.
        {
            PFNode cell = stats.myPath[0];
            stats.myPath.Remove(cell);

            stats.Position = new Vector3(cell.x, cell.y, 0);
        }
        else if (!stats.myPath[0].Walkable)
        {
            return true;
        }

        return stats.myPath.Count == 0;
    }

    //Raycast FoV
    private bool RayPlayerUpdate(Vector3 dir)
    {
        var position = transform.position;
        var hit = Physics2D.Raycast(position, dir, noticeRange, playerAndWall);
        Debug.DrawRay(position, dir, Color.green, 0.1f);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    public override void Act()
    {
        isStateDone = Chase();
        StartCoroutine(MoveToPosition());
        UpdateState();
    }

    private IEnumerator MoveToPosition()
    {
        while (transform.position != stats.Position)
        {
            transform.position = Vector3.MoveTowards(transform.position, stats.Position, Time.deltaTime * speed);
            yield return null;
        }
    }
}
