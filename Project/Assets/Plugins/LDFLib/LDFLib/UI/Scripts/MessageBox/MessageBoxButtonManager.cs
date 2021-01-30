using UnityEngine;
using UnityEngine.UI;
using System;
using LDF.Localization;

namespace LDF.UserInterface.MessageBox
{
    using ButtonOrientation = MessageBoxButton.MessageBoxButtonOrientation;

    public class MessageBoxButtonManager : MonoBehaviour
    {
        public delegate void CloseMessageBox(Action onEnd);
        internal CloseMessageBox CloseMessageBoxCallback;

        [SerializeField]
        private GameObject _horizontalButtons;

        [SerializeField]
        private GameObject _verticalButtons;

        public void ShowButtons(MessageBoxButton[] buttons, ButtonOrientation orientation)
        {
            Transform buttonsParent;
            switch (orientation)
            {
                case ButtonOrientation.None:
                    {
                        _verticalButtons.SetActive(false);
                        _horizontalButtons.SetActive(false);
                        return;
                    }
                case ButtonOrientation.Horizontal:
                    {
                        _verticalButtons.SetActive(false);
                        _horizontalButtons.SetActive(true);
                        buttonsParent = _horizontalButtons.transform;
                    }
                    break;
                case ButtonOrientation.Vertical:
                    {
                        _verticalButtons.SetActive(true);
                        _horizontalButtons.SetActive(false);
                        buttonsParent = _verticalButtons.transform;
                    }
                    break;
                default:
                    {
                        throw new NotImplementedException();
                    }
            }

            //todo: add buttons pooling
            int currentIndex = 0;

            foreach (var button in buttonsParent.GetComponentsInChildren<Button>())
            {
                if (currentIndex >= buttons.Length)
                {
                    button.gameObject.SetActive(false);
                    continue;
                }

                button.gameObject.SetActive(true);
                button.onClick = new Button.ButtonClickedEvent();

                Action onClickEvent = () => { };

                if (buttons[currentIndex].SerializedOnButtonClicked != null)
                {
                    onClickEvent += buttons[currentIndex].SerializedOnButtonClicked.Invoke;
                }

                if (buttons[currentIndex].OnButtonClicked != null)
                {
                    onClickEvent += buttons[currentIndex].OnButtonClicked.Invoke;
                }

                if (buttons[currentIndex].CloseAfterClicked)
                {
                    void OnClickCallback() => CloseMessageBoxCallback(onClickEvent);

                    button.onClick.AddListener(OnClickCallback);
                }
                else
                {
                    button.onClick.AddListener(onClickEvent.Invoke);
                }

                if (buttons[currentIndex].ShouldHaveBackButtonTrigger)
                {
                    button.GetComponent<BackButtonTrigger>().enabled = true;
                }

                var content = button.GetComponentInChildren<TextMeshProLocalization>();
                content.SetAndUpdateLocalizationKey(buttons[currentIndex].Content);

                currentIndex++;
            }
        }
    }
}