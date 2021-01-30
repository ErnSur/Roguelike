using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    private PlayerStats stats; // add requirement for player stats

    //new combat
    public LayerMask NpcLayer;
    public float raycastDistance;
    Transform npc;
    static LayerMask statNpcLayer;
    public LayerMask rayDebugCheckLayer;

    void Start () {
        stats = GetComponent<PlayerStats>();
        statNpcLayer = NpcLayer;
		npc = null;
	}

	void Update () {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
			redStone.SpawnField(PlayerStats.instance.Position, "right");

            npc = RayNpcUpdate(Vector3.right, raycastDistance);
            if (npc != null)
            {
                stats.DealDamage(npc.GetComponent<NPCStats>());
				SkipTurn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
			redStone.SpawnField(PlayerStats.instance.Position, "left");

            npc = RayNpcUpdate(Vector3.left, raycastDistance);
            if (npc != null)
            {
                stats.DealDamage(npc.GetComponent<NPCStats>());
				SkipTurn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
			redStone.SpawnField(PlayerStats.instance.Position, "up");

            npc = RayNpcUpdate(Vector3.up, raycastDistance);
            if (npc != null)
            {
                stats.DealDamage(npc.GetComponent<NPCStats>());
				SkipTurn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
			redStone.SpawnField(PlayerStats.instance.Position, "down");

            npc = RayNpcUpdate(Vector3.down, raycastDistance);
            if (npc != null)
            {
                stats.DealDamage(npc.GetComponent<NPCStats>());
				SkipTurn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            npc = DebugCheck(Vector3.right, raycastDistance);
            if (npc != null)
            {
				Log.Write(npc.name);
            }
        }
    }

    //Raycast for enemy collision
    public static Transform RayNpcUpdate(Vector3 rayDirection, float raycastDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerStats.instance.Position, rayDirection, raycastDistance, statNpcLayer);
        Debug.DrawRay(PlayerStats.instance.Position, rayDirection, Color.green, 1f);

        if (hit.collider != null)
        {
            //Debug.Log("hit " + hit.collider.name);

            return hit.collider.transform;
        }
        return null;
    }

    public Transform DebugCheck(Vector3 rayDirection, float raycastDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerStats.instance.Position, rayDirection, raycastDistance, rayDebugCheckLayer);
        Debug.DrawRay(PlayerStats.instance.Position, rayDirection, Color.green, 1f);

        if (hit.collider != null)
        {
            //Debug.Log("hit " + hit.collider.name);

            return hit.collider.transform;
        }
        return null;
    }

	public void SkipTurn()
	{
		if (TurnSystem.nextTurn != null)
			TurnSystem.nextTurn();
	}

	public RedStone redStone;
}
