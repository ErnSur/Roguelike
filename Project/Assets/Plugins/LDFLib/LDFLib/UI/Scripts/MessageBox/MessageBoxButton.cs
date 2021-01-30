using System;
using UnityEngine;
using UnityEngine.Events;

namespace LDF.UserInterface.MessageBox
{
    [Serializable]
    public class MessageBoxButton
    {
        public Action OnButtonClicked { get; }
        public LocalizedText Content => _localizedText;
        public bool CloseAfterClicked => _closeAfterClicked;
        public bool ShouldHaveBackButtonTrigger => _addBackButtonTrigger;
        public UnityEvent SerializedOnButtonClicked => _serializedOnButtonClicked;

        [SerializeField]
        private bool _closeAfterClicked, _addBackButtonTrigger;
        [SerializeField]
        private LocalizedText _localizedText;
        [SerializeField]
        private UnityEvent _serializedOnButtonClicked;

        public MessageBoxButton(LocalizedText key, bool closeAfterClicked, Action onClicked = null, UnityEvent serializedOnClicked = null)
        {
            OnButtonClicked = onClicked;

            _localizedText = key;
            _closeAfterClicked = closeAfterClicked;
            _serializedOnButtonClicked = serializedOnClicked;
        }
        
        public MessageBoxButton(LocalizedText key, bool closeAfterClicked, UnityEvent serializedOnClicked)
        {
            OnButtonClicked = null;

            _localizedText = key;
            _closeAfterClicked = closeAfterClicked;
            _serializedOnButtonClicked = serializedOnClicked;
        }

        [Serializable]
        public enum MessageBoxButtonOrientation
        {
            None = -1,
            Vertical = 0,
            Horizontal = 1
        }
    }
}