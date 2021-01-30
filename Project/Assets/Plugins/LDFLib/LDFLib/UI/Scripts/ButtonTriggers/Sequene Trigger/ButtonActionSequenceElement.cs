using System.Collections.Generic;
using UnityEngine;

namespace LDF
{
    [RequireComponent(typeof(ButtonActionSequenceTrigger))]
    public abstract class ButtonActionSequenceElement : MonoBehaviour
    {
        public virtual void OnClick(Queue<ButtonActionSequenceElement> triggerSequence)
        {
            if (triggerSequence.Count != 0)
            {
                triggerSequence.Dequeue().OnClick(triggerSequence);
            }
        }
    }
}
