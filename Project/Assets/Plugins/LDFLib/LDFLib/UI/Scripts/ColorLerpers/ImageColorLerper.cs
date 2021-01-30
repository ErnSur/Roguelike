using UnityEngine;
using UnityEngine.UI;
using LDF.Utility;

namespace LDF.UserInterface
{
    [RequireComponent(typeof(Image))]
    public class ImageColorLerper : ColorLerper
    {
        [Required, SerializeField]
        private Image _image;

        [SerializeField]
        private float _lerpDuration = 0.25f;

        protected override float LerpDuration => _lerpDuration;

        protected override Color ColorToLerp
        {
            get => _image.color;
            set => _image.color = value;
        }
    }

}
