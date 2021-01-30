using System;
using UnityEngine.UI;

namespace LDF.Utility
{
    public static class ButtonExtensions
    {
        public static void AddActions(this Button button, params Action[] onClickActions)
        {
            foreach (var action in onClickActions)
            {
                if(action == null)
                {
                    continue;
                }

                button.onClick.AddListener(OnClick);

                void OnClick() => action();
            }
        }

        public static void SetActions(this Button button, params Action[] onClickActions)
        {
            button.onClick = new Button.ButtonClickedEvent();
            button.AddActions(onClickActions);
        }
    }
}
