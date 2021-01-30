using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LDF.UserInterface
{
    [RequireComponent(typeof(ButtonActionSequenceTrigger))]
    public class SerializedButtonAction : ButtonActionSequenceElement
    {
        [SerializeField]
        private UnityEvent _action;

        public override void OnClick(Queue<ButtonActionSequenceElement> triggerSequence)
        {
            _action?.Invoke();

            base.OnClick(triggerSequence);
        }
    }
}