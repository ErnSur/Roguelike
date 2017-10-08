using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {

    public NPCStats npcStats;
    public State[] npcStates;
    public State currentState;

    private void Start()
    {
        //ScriptableObject.CreateInstance<NPCstats>();
        currentState.enabled = true;
        TurnSystem.enemyTurn += OneTimeAction;
    }

    private void Update()
    {
        if(currentState.enabled != true) { currentState.enabled = true; }
    }

    void OneTimeAction()
    {
        currentState.Act();
    }

    private void OnDisable()
    {
        TurnSystem.enemyTurn -= OneTimeAction;
    }
}
