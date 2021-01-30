using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class DropdownSetLanguage : MonoBehaviour
{
    [SerializeField]
    private Color _pressedButtonCollor;

    [SerializeField]
    private RectTransform _dropdownMenu; 

    private Toggle _dropdownToggle;
    private Image _dropdownImage;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _dropdownToggle = GetComponent<Toggle>();
        _dropdownImage = GetComponent<Image>();

        SetupToggle();
    }

    private void SetupToggle()
    {
        _dropdownToggle.onValueChanged = new Toggle.ToggleEvent();
        _dropdownToggle.onValueChanged.AddListener(SetDropDownImageColor);
        _dropdownToggle.onValueChanged.AddListener(ToggleDropdownMenu);
    }

    private void SetDropDownImageColor(bool isButtonPressed)
    {
        if (isButtonPressed)
        {
            _dropdownImage.color = _pressedButtonCollor;
        }
        else
        {
            _dropdownImage.color = Color.white;
        }
    }

    private void ToggleDropdownMenu(bool value)
    {
        _dropdownMenu.gameObject.SetActive(value);
    }
}
