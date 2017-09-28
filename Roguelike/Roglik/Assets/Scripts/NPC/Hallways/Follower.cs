using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

    public PFaStar pathfinding;
    public float speed = 3.5f;
    private const float RAYCAST_DISTANCE = 2f; // One cell
    public Transform player;

    public Vector3 pos;
    public LayerMask wallLayer;

    void MoveOneCell()
    {

        if(transform.position == pos)
        {
            
            List<PFnode> myPath = pathfinding.FindPath(transform, player);

            if (myPath != null)
            {
                PFnode cell = myPath[0];

                pos = new Vector3(cell.x,cell.y,0);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
    }

    //Raycast FoV
    bool RayPlayerUpdate(Vector3 dir)
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, RAYCAST_DISTANCE, wallLayer);
        Debug.DrawRay(transform.position, dir, Color.green, 0.1f);

        if (hit.collider != null)
        {
            Debug.Log("hit " + hit.collider.name);
            return true;
        }
        return false;
    }

    void Update () {
        Vector3 playerDir = player.transform.position - transform.position;
        if (!RayPlayerUpdate(playerDir))
        {
            MoveOneCell();
        }
	}

	void Start () {
        pos = transform.position;

    }
}
