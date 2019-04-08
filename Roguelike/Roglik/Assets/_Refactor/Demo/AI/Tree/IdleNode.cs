using System;
using UnityEngine;

namespace LDF.Systems.AI
{
    [CreateAssetMenu(menuName = "AI/Nodes/Idle")]
    public class IdleNode : AINode
    {
        public string Name = "Default Name";

        protected override void OnInit()
        {
            Debug.Log($"Inited {Name}");
        }

        protected override void OnTick(Action onEnd)
        {
            Debug.Log($"Idling");
            onEnd?.Invoke();
        }
    }
}
