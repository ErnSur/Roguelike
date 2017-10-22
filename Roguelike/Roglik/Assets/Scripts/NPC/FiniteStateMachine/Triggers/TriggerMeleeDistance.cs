using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Trigger/On Melee Distance")]
public class TriggerMeleeDistance : Trigger {

    public override State TriggerEvent(Vector3 watcher, Vector3 target, bool isStateDone)
    {
        float distance = Vector3.Distance(target, watcher);

        if(distance <= 1f) // 1f is one cell means melee distance
        {
            return stateToReturn;
        }
        return null;
    }
}

