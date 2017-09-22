using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASgrid : MonoBehaviour {

    public Vector2 gridWorldSize;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, -1));
    }
}
