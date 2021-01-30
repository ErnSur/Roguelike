using UnityEngine;
using FluentBehaviourTree;
using LDF.Roglik;

namespace LDF.Systems.AI
{
    public class AIController : TurnSystemBehaviour
    {
        [SerializeField]
        private AITree _serializedTree;

        [SerializeField]
        private LivingBodySystem _playerBody;

        [SerializeField]
        private AIMovement_S ai;

        private IBehaviourTreeNode node;

        protected override void Awake()
        {
            base.Awake();

            _serializedTree = Instantiate(_serializedTree);

            _serializedTree.Initialize(new AIBehaviourArgs());

            node = GetStartingNode();
        }

        protected override void OnTurnStart(Turn turn)
        {
            if (turn == Turn.Player)
                return;
            ai.DoTurnStart(turn);
        }

        protected override void OnTurnUpdate(Turn turn)
        {
            if (turn == Turn.Player)
                return;
            node.Tick(Time.deltaTime);
        }

        public IBehaviourTreeNode GetStartingNode()
        {
            var tree = new BehaviourTreeBuilder()
                .Selector("Do Rogue Tasks")
                    .Sequence("Be Aggresive")
                        .If(SeePlayer)
                        .Do(Chase)
                        .Do(Attack)
                        .End()
                    .Sequence("Look Busy")
                        .Do(Patrol)
                        .Do(LookBusy)
                        .Do(ChooseNewPatrol)
                        .End()
                .End()
                .Build();

            return tree;
        }

        public BehaviourTreeStatus Chase(TimeData t)
        {
            Debug.Log($"Chase");
            if (ai.ReachedGoal())
            {
                return BehaviourTreeStatus.Success;
            }
            else if (ai.DoTickMovement())
            {
                ai.DoEndTurn("Moved");
                return BehaviourTreeStatus.Running;
            }

            return BehaviourTreeStatus.Running;
        }

        public BehaviourTreeStatus Patrol(TimeData t)
        {
            Debug.Log($"Patrol");
            throw new System.NotImplementedException();
        }

        public BehaviourTreeStatus LookBusy(TimeData t)
        {
            Debug.Log($"Look Busy");
            return BehaviourTreeStatus.Running;
        }
        public BehaviourTreeStatus ChooseNewPatrol(TimeData t)
        {
            Debug.Log($"Choose new patrol");
            return BehaviourTreeStatus.Success;
        }

        public BehaviourTreeStatus Attack(TimeData t)
        {
            Debug.Log($"Attack");
            _playerBody.TakeAttack(2);
            ai.DoEndTurn("Attacked ciach");
            return BehaviourTreeStatus.Running;
        }

        public bool IsNearPlayer(TimeData t)
        {
            Debug.Log($"Is nearPlayer");
            return true;
        }

        public bool SeePlayer(TimeData t)
        {
            Debug.Log($"SeePlayer");
            return true;
        }
    }
}
