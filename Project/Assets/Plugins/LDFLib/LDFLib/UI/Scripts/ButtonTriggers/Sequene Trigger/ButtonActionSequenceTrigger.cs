using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LDF.Utility;

namespace LDF
{
    [RequireComponent(typeof(Button))]
    public class ButtonActionSequenceTrigger : MonoBehaviour
    {
        [SerializeField]
        private List<ButtonActionSequenceElement> _buttonTriggers;

        private void Awake()
        {
            GetComponent<Button>().AddActions(OnClick);
        }

        private void OnClick()
        {
            var triggerQueue = new Queue<ButtonActionSequenceElement>(_buttonTriggers);

            triggerQueue.Dequeue().OnClick(triggerQueue);
        }
    }
}
