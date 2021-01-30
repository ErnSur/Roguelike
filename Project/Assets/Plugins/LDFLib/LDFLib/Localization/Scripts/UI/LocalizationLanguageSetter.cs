using UnityEngine;
using UnityEngine.UI;

namespace LDF.Localization
{
    [RequireComponent(typeof(Toggle))]
    public class LocalizationLanguageSetter : MonoBehaviour
    {
        [SerializeField]
        private SystemLanguage _language;
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();

            UpdateToggleState();

            SetToggleTriggerAction();
        }

        private void SetToggleTriggerAction()
        {
            _toggle.onValueChanged.AddListener((v) => SetLanguage());
        }

        private void UpdateToggleState()
        {
            if (LocalizationSystem.CurrentLanguage == _language)
            {
                _toggle.isOn = true;
            }
        }

        public void SetLanguage()
        {
            LocalizationSystem.SetCurrentLanguage(_language);
        }
    }
}