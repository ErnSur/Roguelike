using System;
using UnityEngine;

namespace LDF.Utility.SequenceTrigger
{
    public abstract class SequenceTrigger<TAction, TActionArgs> : MonoBehaviour
        where TAction : SequenceAction<TActionArgs>
        where TActionArgs : SequenceArgs
    {
        [SerializeField, ExtendedList]
        private TAction[] _onTriggerActions;

        private TAction _currentAction;

        public bool IsSequenceInProgress { get; private set; }

        public event Action<TActionArgs> OnSequenceEndEvent;

        public void StartSequence(TActionArgs args)
        {
            if (_onTriggerActions == null || _onTriggerActions.Length == 0)
            {
                return;
            }

            IsSequenceInProgress = true;

            for (int i = 0; i < _onTriggerActions.Length - 1; i++)
            {
                var localIndex = i;

                _onTriggerActions[i].Initialize(FireNextActionInSequence, EndThisSequence);

                void FireNextActionInSequence()
                {
                    _currentAction = _onTriggerActions[localIndex + 1];
                    _currentAction.OnTrigger(args);
                }
            }

            _onTriggerActions[_onTriggerActions.Length - 1].Initialize(EndThisSequence, EndThisSequence);

            _currentAction = _onTriggerActions[0];
            _currentAction.OnTrigger(args);

            void EndThisSequence()
            {
                OnSequenceEndEvent?.Invoke(args);
                IsSequenceInProgress = false;
                _currentAction = null;
            }
        }

        public void StopSequence()
        {
            if(_currentAction != null)
            {
                _currentAction.StopAction();
            }
        }
    }
}