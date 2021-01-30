using UnityEngine;

namespace LDF.Systems.AI
{
    public abstract class BehaviourTrigger : ScriptableObject
    {
        public abstract bool IsTriggered();
    }

    public abstract class BehaviourTrigger<TArgs> : BehaviourTrigger
    {
        protected TArgs args;
    }
}
