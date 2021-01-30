using TMPro;
using UnityEngine;
using LDF.Utility;

namespace LDF.Localization
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    [AddComponentMenu("UI/TextMeshPro - Localization", 11)]
    public class TextMeshProLocalization : MonoBehaviour
    {
        [Required, SerializeField]
        private TMP_Text _textMeshPro;

        [SerializeField]
        private LocalizedText _localizedText;

        private void OnEnable()
        {
            SetText();
            LocalizationSystem.LocalizationChangedEvent += SetText;
        }

        private void OnDisable()
        {
            LocalizationSystem.LocalizationChangedEvent -= SetText;
        }

        public void SetAndUpdateLocalizationKey(LocalizedText key)
        {
            _localizedText = key;
            SetText();
        }

        private void SetText()
        {
            _textMeshPro.text = LocalizationSystem.GetLocalizedString(_localizedText);
        }

        public void UpdateText(string prefix, string suffix)
        {
            _textMeshPro.text = $"{prefix}{LocalizationSystem.GetLocalizedString(_localizedText)}{suffix}";
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(_localizedText != null && _textMeshPro != null)
            {
                SetText();
            }
        }
#endif
    }
}