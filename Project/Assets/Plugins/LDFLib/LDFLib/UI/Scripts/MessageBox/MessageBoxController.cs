using UnityEngine;
using UnityEngine.UI;
using LDF.UserInterface.Animations;
using System;
using LDF.Localization;

namespace LDF.UserInterface.MessageBox
{
    using LocalizedText = TextMeshProLocalization;

    [RequireComponent(typeof(IToggleable))]
    [RequireComponent(typeof(Image))]
    public class MessageBoxController : MonoBehaviour
    {
        public event Action<GameObject> OnHide;

        [SerializeField]
        private Image _icon;
        [SerializeField]
        private LocalizedText _headline;
        [SerializeField]
        private LocalizedText _content;
        [SerializeField]
        private MessageBoxButtonManager _buttons;

        private IToggleable _stateAnimator;
        private Image _raycastBlockingImage;

        private ResizeToTextSize _headlineResizer;
        private ResizeToTextSize _contentResizer;

        private void Awake()
        {
            CollectComponents();
            _buttons.CloseMessageBoxCallback = Hide;
        }

        private void CollectComponents()
        {
            _raycastBlockingImage = GetComponent<Image>();
            _stateAnimator = GetComponent<IToggleable>();

            _headlineResizer = _headline.GetComponent<ResizeToTextSize>();
            _contentResizer = _content.GetComponent<ResizeToTextSize>();
        }

        public void ShowMessageBox(MessageBoxContent content, Action showedCallback)
        {
            OnHide = null;
            _raycastBlockingImage.raycastTarget = true;

            _icon.sprite = content.Icon;
            _headline.SetAndUpdateLocalizationKey(content.Headline);
            _content.SetAndUpdateLocalizationKey(content.Content);
            _buttons.ShowButtons(content.Buttons, content.ButtonOrientation);

            _headlineResizer.UpdateSize();
            _contentResizer.UpdateSize();

            _stateAnimator.Show(showedCallback);
        }

        public void Hide(Action onEnd)
        {
            _raycastBlockingImage.raycastTarget = false;

            _stateAnimator.Hide(OnAnimationEnd);

            void OnAnimationEnd()
            {
                OnHide(this.gameObject);
                onEnd?.Invoke();
            }
        }
    }
}