using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASgridInfo : MonoBehaviour {

    public Grid myGrid;

    private float cellsize;
    

	// Use this for initialization
	void Start () {
        cellsize = myGrid.cellSize.x;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
