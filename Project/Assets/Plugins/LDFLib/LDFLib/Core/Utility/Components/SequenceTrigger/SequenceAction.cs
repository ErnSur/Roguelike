using System;
using UnityEngine;

namespace LDF.Utility.SequenceTrigger
{
    public abstract class SequenceAction<TArgs> : MonoBehaviour where TArgs : SequenceArgs
    {
        private Action _moveSequence, _endSequence;

        internal void Initialize(Action moveSequence, Action endSequence)
        {
            _moveSequence = moveSequence;
            _endSequence = endSequence;
        }

        public abstract void OnTrigger(TArgs args);

        public virtual void StopAction()
        {
            StopAllCoroutines();
            EndSequence();
        }

        protected void MoveSequence() => _moveSequence?.Invoke();
        protected void EndSequence() => _endSequence?.Invoke();
    }
}