using System;
using UnityEngine;

namespace LDF.Systems.AI
{
    public abstract class BehaviourNode: ScriptableObject
    {
        [SerializeField]
        private TriggerLink[] _triggerLinks;

        public bool IsDone { get; private set; }

        protected internal abstract void OnInit();

        protected internal abstract void OnTick(Action onEnd);

        public void CreateTriggers()
        {
            Debug.Log($"{name} CreateTriggers");
            foreach (var link in _triggerLinks)
            {
                link.Trigger = Instantiate(link.Trigger);
            }
        }

        internal bool TryGetNewNode<T>(ref T nextNode) where T : BehaviourNode
        {
            foreach (var link in _triggerLinks)
            {
                if (link.Trigger.IsTriggered())
                {
                    nextNode = link.NextNode as T;
                    return true;
                }
            }

            return false;
        }

        [Serializable]
        public class TriggerLink
        {
            public BehaviourTrigger Trigger;
            public BehaviourNode NextNode;
        }
    }

    public abstract class BehaviourNode<TInput> : BehaviourNode
    {
        protected internal TInput input;
    }
}
