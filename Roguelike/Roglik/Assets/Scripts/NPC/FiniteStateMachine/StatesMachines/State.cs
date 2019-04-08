using UnityEngine;

public abstract class State : MonoBehaviour
{
    [System.Serializable]
    public struct Triggers
    {
        public State StateToTrigger;
        public Trigger trigger;
    }

    [HideInInspector]
    public bool isStateDone;

    [HideInInspector]
    public StateController controller;

    [HideInInspector]
    public NPCStats stats;

    public Triggers[] triggers;

    public abstract void Act();

    public virtual void UpdateState()
    {
        foreach (Triggers trigger in triggers)
        {
            var triggered = trigger.trigger.TriggerEvent(stats.Position, PlayerStats.instance.Position, isStateDone);

            if (triggered)
            {
                controller.currentState = trigger.StateToTrigger;
                this.enabled = false;
                return;
            }
        }
    }

    void Awake()
    {
        controller = GetComponent<StateController>();
        stats = GetComponent<NPCStats>();
    }
}
