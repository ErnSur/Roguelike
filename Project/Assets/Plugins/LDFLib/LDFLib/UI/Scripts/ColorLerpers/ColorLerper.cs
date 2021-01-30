using System.Collections;
using UnityEngine;

namespace LDF.UserInterface
{
    public abstract class ColorLerper : MonoBehaviour
    {
        [SerializeField]
        protected Color[] colors;

        protected abstract Color ColorToLerp { get; set; }
        protected abstract float LerpDuration { get; }

        private int? _activeCoroutineColorTarget;

        private void OnDisable()
        {
            ForceFinishCurrentLerp();
        }

        private void ForceFinishCurrentLerp()
        {
            if (_activeCoroutineColorTarget != null)
            {
                SetColor(_activeCoroutineColorTarget.Value);
                _activeCoroutineColorTarget = null;
            }
        }

        public void SetColor(int colorIndex)
        {
            ColorToLerp = colors[colorIndex];
        }

        public void SetColor(bool value)
        {
            var colorIndex = value ? 1 : 0;

            SetColor(colorIndex);
        }

        public void LerpToColor(int colorIndex)
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(LerpColorTo(colorIndex));
            }
            else
            {
                SetColor(colorIndex);
            }
        }

        public void LerpToColor(bool value)
        {
            var index = value ? 1 : 0;
            LerpToColor(index);
        }

        private IEnumerator LerpColorTo(int colorIndex)
        {
            _activeCoroutineColorTarget = colorIndex;

            var startColor = ColorToLerp;
            var endColor = colors[colorIndex];

            for (float t = 0; t < 1f; t += Time.deltaTime / LerpDuration)
            {
                ColorToLerp = Color.Lerp(startColor, endColor, t);
                yield return null;
            }
            ColorToLerp = endColor;

            _activeCoroutineColorTarget = null;
        }
    }

}
