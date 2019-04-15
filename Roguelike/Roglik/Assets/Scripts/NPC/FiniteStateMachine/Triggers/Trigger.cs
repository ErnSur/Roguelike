using UnityEngine;

namespace LDF.SOBT
{
    public abstract class Trigger : ScriptableObject
    {
        public abstract bool TriggerEvent(Vector3 watcher, Vector3 target, bool isStateDone);
    }
}