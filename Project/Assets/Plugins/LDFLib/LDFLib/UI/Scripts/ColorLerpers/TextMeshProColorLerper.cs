using TMPro;
using UnityEngine;
using LDF.Utility;

namespace LDF.UserInterface
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMeshProColorLerper : ColorLerper
    {
        [Required, SerializeField]
        private TextMeshProUGUI _textMeshPro;

        [SerializeField]
        private float _lerpDuration = 0.25f;

        protected override float LerpDuration => _lerpDuration;

        protected override Color ColorToLerp
        {
            get => _textMeshPro.color;
            set => _textMeshPro.color = value;
        }
    }
}
