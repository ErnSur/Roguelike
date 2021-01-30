using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LDF.UserInterface
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("UI/Back Button Trigger")]
    public class BackButtonTrigger : MonoBehaviour
    {
        private Button _button;
        private const string _backButtonInputKey = "Cancel";
        private static List<Button> _backButtonsActiveOnScene = new List<Button>();

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _backButtonsActiveOnScene.Insert(0, _button);
        }

        private void OnDisable()
        {
            _backButtonsActiveOnScene.Remove(_button);
        }

        private void Update()
        {
            ListenToBackButtonPress();
        }

        private void ListenToBackButtonPress()
        {
            if (Input.GetButtonDown(_backButtonInputKey) && _backButtonsActiveOnScene.Count > 0)
            {
                _backButtonsActiveOnScene[0].onClick.Invoke();
            }
        }
    }
}