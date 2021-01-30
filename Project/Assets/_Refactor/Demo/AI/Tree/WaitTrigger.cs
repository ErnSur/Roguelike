using UnityEngine;

namespace LDF.Systems.AI
{
    [CreateAssetMenu(menuName = "AI/Triggers/Wait")]
    public class WaitTrigger : AITrigger
    {
        public int Turns = 2;

        public override bool IsTriggered()
        {
            Debug.Log($"Waiting for {Turns} turns");

            return Turns-- <= 0;
        }
    }
}
