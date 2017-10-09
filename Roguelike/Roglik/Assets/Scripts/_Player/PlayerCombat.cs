using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    private PlayerStats stats; // add requirement for player stats

    //new combat
    public LayerMask NpcLayer;
    private static LayerMask statNpcLayer;
    public float raycastDistance;
    Transform npc;

    void Start () {
        stats = GetComponent<PlayerStats>();
        statNpcLayer = NpcLayer;
        npc = null;
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.RightArrow) && RayNpcUpdate(Vector3.right, raycastDistance) != null)
        {
            npc = RayNpcUpdate(Vector3.right, raycastDistance);
            if (npc != null)
            {
                npc.GetComponent<CharacterStats>().TakeDamage(stats.dmg);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            npc = RayNpcUpdate(Vector3.left, raycastDistance);
            if (npc != null)
            {
                npc.GetComponent<CharacterStats>().TakeDamage(stats.dmg);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && RayNpcUpdate(Vector3.up, raycastDistance) != null)
        {
            npc = RayNpcUpdate(Vector3.up, raycastDistance);
            if (npc != null)
            {
                npc.GetComponent<CharacterStats>().TakeDamage(stats.dmg);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && RayNpcUpdate(Vector3.down, raycastDistance) != null)
        {
            npc = RayNpcUpdate(Vector3.down, raycastDistance);
            if (npc != null)
            {
                npc.GetComponent<CharacterStats>().TakeDamage(stats.dmg);
            }
        }
    }

    //Raycast for enemy collision
    public static Transform RayNpcUpdate(Vector3 rayDirection, float raycastDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerMovement.PlayerPos3, rayDirection, raycastDistance, statNpcLayer);
        Debug.DrawRay(PlayerMovement.PlayerPos3, rayDirection, Color.green, 1f);

        if (hit.collider != null)
        {
            Debug.Log("hit " + hit.collider.name);
            
            return hit.collider.transform;
        }
        return null;
    }
}
