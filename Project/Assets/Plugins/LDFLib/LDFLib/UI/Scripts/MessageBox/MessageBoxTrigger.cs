using UnityEngine;

namespace LDF.UserInterface.MessageBox
{
    public class MessageBoxTrigger : MonoBehaviour
    {
        [SerializeField]
        [Header("Should multiple message boxes be displayed?")]
        private bool _multipleActive;
        [SerializeField]
        private MessageBoxContent _messageBoxContent;

        private bool _currentActive;

        public void Show()
        {
            if (!_multipleActive && _currentActive)
            {
                return;
            }

            _currentActive = true;
            MessageBox.Show(_messageBoxContent, null, OnHide);
        }

        void OnHide()
        {
            _currentActive = false;
        }
    }
}