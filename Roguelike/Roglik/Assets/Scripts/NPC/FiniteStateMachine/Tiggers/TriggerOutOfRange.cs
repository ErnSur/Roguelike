using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trigger/Out Of Range")]
public class TriggerOutOfRange : Trigger {

    public float range;


    public override State TriggerEvent(Vector3 watcher, Vector3 target)
    {
        float distance = Vector3.Distance(target, watcher);

        if (distance > range) // 1f is one cell means melee distance
        {
            return stateToReturn;
        }
        return null;
    }
}
