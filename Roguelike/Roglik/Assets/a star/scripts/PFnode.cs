using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFnode : ScriptableObject {

    public int gCost = 0;
    public int hCost = 0;

    public float x;
    public float y;

    public bool walkable;
    public PFnode cameFrom;

    public PFnode(float _x, float _y, bool _walkable)
	{
        walkable = _walkable;
        x = _x;
        y = _y;
    }

    public int fCost
    {
        get { return gCost + hCost; }
    }

}
