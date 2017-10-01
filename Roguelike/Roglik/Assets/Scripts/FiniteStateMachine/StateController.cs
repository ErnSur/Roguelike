using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {

    public State currentState;
    public NPCstats npcStats;
    public PFaStar pathfinding;

    private bool aiActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!aiActive)
            return;

        currentState.UpdateState(this);
	}
}
