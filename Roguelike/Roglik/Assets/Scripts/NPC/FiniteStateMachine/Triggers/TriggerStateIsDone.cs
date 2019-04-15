using LDF.SOBT;
using UnityEngine;

[CreateAssetMenu(menuName = "Trigger/State Is Done")]
public class TriggerStateIsDone : Trigger
{
    public override bool TriggerEvent(Vector3 watcher, Vector3 target, bool isStateDone)
    {
        return isStateDone;
    }
}