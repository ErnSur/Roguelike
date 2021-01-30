using System;
using UnityEngine;
using LDF.UserInterface.Animations;
using LDF.Utility;

namespace LDF.UserInterface.MVC
{
    [RequireComponent(typeof(IToggleable))]
    public abstract class View : MonoBehaviour
    {
        internal event Action OnShowEvent;
        internal event Action OnShowEndEvent;
        internal event Action OnHideEvent;

        private IToggleable _stateAnimator;

        protected internal virtual void OnInitialize()
        {
            _stateAnimator = GetComponent<IToggleable>();
        }

        protected internal virtual void OnShow()
        {
            _stateAnimator.Show(OnShowEnd);

            OnShowEvent?.Invoke();
        }

        protected internal virtual void OnHide()
        {
            _stateAnimator.Hide();

            OnHideEvent?.Invoke();
        }

        private void OnShowEnd() => OnShowEndEvent?.Invoke();
    }

    public abstract class View<TModel> : View
    {
        [field: Required, SerializeField]
        protected TModel Model { get; private set; }
    }
}