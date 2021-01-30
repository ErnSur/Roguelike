using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LDF.Utility;

namespace LDF.UserInterface
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleSwitchAnimation : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _switchDot;
        
        [SerializeField]
        private float _transitionDuration = 0.25f;

        [SerializeField]
        private float _activePositionOffset;

        [Header("Toggle Value Labels")]
        [SerializeField]
        private ColorLerper _onLabel;
        [SerializeField]
        private ColorLerper _offLabel;
        
        private Toggle _toggle;
        
        private Vector2 _dotOnPosition => new Vector2(_activePositionOffset, 0);
        private Vector2 _dotOffPosition => new Vector2(-_activePositionOffset, 0);

        private void Awake()
        {
            InitToggleComponent();
            ResetToggle();
        }

        private void OnEnable()
        {
            ResetToggle();
        }

        private void ResetToggle()
        {
            _toggle.interactable = true;
            SetCurrentDotPosition();
            SetLabelsColors();
        }

        private void InitToggleComponent()
        {
            this.Lazy(ref _toggle);

            _toggle.onValueChanged.AddListener(VisualizeValueChange);

            void VisualizeValueChange(bool value)
            {
                StartCoroutine(LerpDotPosition(value));
                LerpLabelsColors();
            }
        }

        private void SetCurrentDotPosition()
        {
            _switchDot.anchoredPosition = GetDotStatePosition(_toggle.isOn);
        }

        private void SetLabelsColors()
        {
            if (_onLabel != null)
            {
                _onLabel.SetColor(_toggle.isOn);
            }

            if (_offLabel != null)
            {
                _offLabel.SetColor(_toggle.isOn);
            }
        }

        private void LerpLabelsColors()
        {
            if (_onLabel != null)
            {
                _onLabel.LerpToColor(_toggle.isOn);
            }
            if (_offLabel != null)
            {
                _offLabel.LerpToColor(_toggle.isOn);
            }
        }

        private IEnumerator LerpDotPosition(bool toggleValue)
        {
            _toggle.interactable = false;

            var dotStartPos = GetDotStatePosition(!toggleValue);

            var dotEndPos = GetDotStatePosition(toggleValue);

            for (float t = 0; t < 1f; t += Time.deltaTime / _transitionDuration)
            {
                _switchDot.anchoredPosition = Vector2.Lerp(dotStartPos, dotEndPos, t);
                yield return null;
            }
            _switchDot.anchoredPosition = dotEndPos;

            _toggle.interactable = true;
        }

        private Vector2 GetDotStatePosition(bool state)
        {
            return state ? _dotOnPosition : _dotOffPosition;
        }
    }
}
