using System;
using UnityEngine;
using UnityEngine.UI;

namespace LDF.UserInterface.Animations
{
    public abstract class Animated : MonoBehaviour, IToggleable
    {
        public virtual bool InProgress { get; protected set; }

        public virtual bool IsOrWillBeVisible { get; }

        public abstract void Show(Action onEnd);
        public abstract void Hide(Action onEnd);
        public abstract void Toggle(Action onEnd);

        #region Unity Inspector Actions

        public void Show()
        {
            Show(null);
        }

        public void Hide()
        {
            Hide(null);
        }

        public void Toggle()
        {
            Toggle((Action)null);
        }

        public void ToggleInverted(bool hide) => Toggle(!hide);

        public void Toggle(bool show)
        {
            if (show)
            {
                Show(null);
            }
            else
            {
                Hide(null);
            }
        }

        /// <summary>
        /// Toggle Animated component and disable interaction on the selectable during animation.
        /// </summary>
        public void Toggle(Selectable selectable)
        {
            selectable.interactable = false;

            Toggle(MakeControlActive);

            void MakeControlActive() => selectable.interactable = true;
        }

        #endregion

#if UNITY_EDITOR
        [ContextMenu("Show")]
        public void Show_EditorOnly() => Show(null);

        [ContextMenu("Hide")]
        public void Hide_EditorOnly() => Hide(null);
#endif
    }
}