using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerSight : Trigger {

    public LayerMask playerAndWall;
    public float noticeRange;
    public State tiggerState;

    public override State TriggerEvent(Vector3 watcher, Vector3 target)
    {
        Vector3 dir = target - watcher;

        RaycastHit2D hit = Physics2D.Raycast(watcher, dir, noticeRange, playerAndWall);
        Debug.DrawRay(watcher, dir, Color.green, 0.1f);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("hit " + hit.collider.name);
            return tiggerState;
        }
        return null;
    }
}
