using UnityEngine;

[CreateAssetMenu(menuName = "Trigger/Out Of Range")]
public class TriggerOutOfRange : Trigger
{
    public float range;

    public override bool TriggerEvent(Vector3 watcher, Vector3 target, bool isStateDone)
    {
        float distance = Vector3.Distance(target, watcher);

        if (distance > range) // 1f is one cell means melee distance
        {
            return true;
        }
        return false;
    }
}
