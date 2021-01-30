using TMPro;
using UnityEngine;
using LDF.Utility;

namespace LDF.UserInterface
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ResizeToTextSize : MonoBehaviour
    {
        [SerializeField]
        private bool _resizeOnRectTransformDimensionsChange = true;
        [SerializeField]
        private Vector2 _margin;

        private RectTransform _rectTransform;
        private TextMeshProUGUI _textComponent;

        public RectTransform RectTransform => this.Lazy(ref _rectTransform);
        private TextMeshProUGUI _TextComponent => this.Lazy(ref _textComponent);

        private void OnRectTransformDimensionsChange()
        {
            if (_resizeOnRectTransformDimensionsChange)
            {
                UpdateSize();
            }
        }

        public void UpdateSize()
        {
            RectTransform.sizeDelta = _TextComponent.GetPreferredValues(_TextComponent.text) + _margin;
        }
    }
}