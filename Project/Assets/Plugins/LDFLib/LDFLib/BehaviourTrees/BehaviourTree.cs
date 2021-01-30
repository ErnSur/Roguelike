using System;
using UnityEngine;
using LDF.Utility;

namespace LDF.Systems.AI
{
    public abstract class BehaviourTree<TNode,TInput> : ScriptableObject
        where TNode : BehaviourNode<TInput>
    {
        [field: SerializeField]
        public TNode StartingNode { get; private set; }

        [SerializeField, ReadOnly]
        private TNode _currentNode;

        private TInput _input;

        public void Initialize(TInput input)
        {
            _input = input;
            SetAndInitCurrentNode(StartingNode);
        }

        private void SetAndInitCurrentNode(TNode nodePrototype)
        {
            _currentNode = Instantiate(nodePrototype);
            _currentNode.input = _input;
            _currentNode.CreateTriggers();
            _currentNode.OnInit();
        }

        public void UpdateBehaviour(Action onEnd)
        {
            _currentNode.OnTick(TryChangeNode);

            void TryChangeNode()
            {
                Debug.Log($"Try change Node");

                onEnd?.Invoke();
                if(_currentNode.TryGetNewNode(ref _currentNode))
                {
                    SetAndInitCurrentNode(_currentNode);
                }
            }
        }
    }
}
