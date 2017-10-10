using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Trigger : ScriptableObject {

    [HideInInspector]public State stateToReturn;
    public abstract State TriggerEvent(Vector3 watcher, Vector3 target);
}
