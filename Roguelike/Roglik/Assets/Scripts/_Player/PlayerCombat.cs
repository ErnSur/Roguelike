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

    void Start () {
        stats = GetComponent<PlayerStats>();
        statNpcLayer = NpcLayer;
		npc = null;
	}

	void Update () {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
				redStone.SpawnField(PlayerStats.instance.Position);
            npc = RayNpcUpdate(Vector3.right, raycastDistance);
            if (npc != null)
            {
                stats.DealDamage(npc.GetComponent<NPCStats>());
				TurnSystem.nextTurn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            npc = RayNpcUpdate(Vector3.left, raycastDistance);
            if (npc != null)
            {
                stats.DealDamage(npc.GetComponent<NPCStats>());
				TurnSystem.nextTurn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            npc = RayNpcUpdate(Vector3.up, raycastDistance);
            if (npc != null)
            {
                stats.DealDamage(npc.GetComponent<NPCStats>());
				TurnSystem.nextTurn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            npc = RayNpcUpdate(Vector3.down, raycastDistance);
            if (npc != null)
            {
                stats.DealDamage(npc.GetComponent<NPCStats>());
				TurnSystem.nextTurn();
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

	public RedStone redStone;
}
