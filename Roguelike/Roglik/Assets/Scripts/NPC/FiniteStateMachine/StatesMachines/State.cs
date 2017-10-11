using UnityEngine;

public abstract class State : MonoBehaviour {
    [System.Serializable]
    public struct Triggers
    {
        public State StateToTrigger;
        public Trigger trigger;
    }


	[HideInInspector]public bool isStateDone;
    [HideInInspector]public StateController controller;
    [HideInInspector]public NPCStats stats;
    public Triggers[] triggers;


    public abstract void Act();

    public virtual void UpdateState()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].trigger.stateToReturn = triggers[i].StateToTrigger;

            State newState = triggers[i].trigger.TriggerEvent(stats.Position, PlayerStats.instance.Position, isStateDone);
            if (newState != null)
            {
                controller.currentState = newState;
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
