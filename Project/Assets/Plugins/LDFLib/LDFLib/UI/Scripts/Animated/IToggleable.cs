using System;

namespace LDF.UserInterface.Animations
{
    public interface IToggleable
    {
        bool InProgress { get; }
        bool IsOrWillBeVisible { get; }

        void Show(Action onEnd = null);
        void Hide(Action onEnd = null);
        void Toggle(Action onEnd = null);
    }
}