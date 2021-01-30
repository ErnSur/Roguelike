using System;
using UnityEngine;

namespace LDF.UserInterface.MessageBox
{
    using ButtonOrientation = MessageBoxButton.MessageBoxButtonOrientation;

    [Serializable]
    public class MessageBoxContent
    {
        public Sprite Icon;
        public LocalizedText Content;
        public LocalizedText Headline;
        public MessageBoxButton[] Buttons;
        public ButtonOrientation ButtonOrientation;

        public MessageBoxContent(LocalizedText content, LocalizedText headline, MessageBoxButton[] buttons, ButtonOrientation orientation, Sprite icon = null)
        {
            Icon = icon;
            Buttons = buttons;
            Content = content;
            Headline = headline;
            ButtonOrientation = orientation;
        }
    }
}