using UnityEngine.UI;
using System;

namespace LDF.Utility
{
    public static class ToggleExtensions
    {
        public static void InitState<T>(this Toggle toggle, T offValue, T onValue, T currentValue, Action<T> setValue)
        {
            toggle.isOn = currentValue.Equals(onValue);
            
            toggle.onValueChanged.AddListener(SwitchValue);
            
            void SwitchValue(bool value)
            {
                T newValue = value ? onValue : offValue;
                
                setValue(newValue);
            }
        }

        public static void AddAction(this Toggle toggle, params Action[] onClickActions)
        {
            foreach (var action in onClickActions)
            {
                toggle.onValueChanged.AddListener(OnClick);

                void OnClick(bool v) => action();
            }
        }

        public static void AddAction(this Toggle toggle, params Action<bool>[] onValueChangedActions)
        {
            foreach (var action in onValueChangedActions)
            {
                toggle.onValueChanged.AddListener(OnValueChanged);

                void OnValueChanged(bool v) => action(v);
            }
        }
    }
}