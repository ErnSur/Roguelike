using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFNode
{
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost => GCost + HCost;
    
    public bool Walkable { get; set; }

    public readonly int x;
    public readonly int y;

    public PFNode cameFrom;

    public PFNode(int x, int y, bool walkable)
	{
        Walkable = walkable;
        this.x = x;
        this.y = y;
    }
}
