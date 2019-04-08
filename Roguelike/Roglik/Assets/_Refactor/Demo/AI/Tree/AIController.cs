using UnityEngine;

namespace LDF.Systems.AI
{
    public class AIController : TurnSystemBehaviour
    {
        [SerializeField]
        private AITree _tree;

        protected override void Awake()
        {
            base.Awake();

            _tree = Instantiate(_tree);

            _tree.Initialize(new AIBehaviourArgs());
        }

        protected override void OnTurnStart(Turn turn)
        {
            if (turn == Turn.Player)
                return;

            _tree.UpdateBehaviour(null);
        }
    }
}
