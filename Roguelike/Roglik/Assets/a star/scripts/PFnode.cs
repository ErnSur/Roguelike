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

    public SpriteRenderer myCell;

     public PFnode(float _x, float _y, bool _walkable, Transform parent, SpriteRenderer cellPrefab) {

        walkable = _walkable;
        x = _x;
        y = _y;

        /*
        myCell = Instantiate(cellPrefab, new Vector3(x, y, -1f), Quaternion.identity, parent) as SpriteRenderer;
        if (walkable)
        {
            Color color = Color.green;
            color.a = 0.4f;
            myCell.color = color;
            //Instantiate(greenCell, new Vector3(x, y, -1f), Quaternion.identity, parent);
        }
        else
        {
            Color color = Color.red;
            color.a = 0.4f;
            myCell.color = color;
            //Instantiate(redCell, new Vector3(x, y, -1f), Quaternion.identity, parent);
        }
        */
    }

    public int fCost
    {
        get { return gCost + hCost; }
    }

}
