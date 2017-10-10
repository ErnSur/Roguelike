using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {

    public State currentState;

    private void Start()
    {
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
