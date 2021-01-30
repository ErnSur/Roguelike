using System;
using UnityEngine;
using LDF.Utility;

namespace LDF.UserInterface.MVC
{
    public abstract class Controller : MonoBehaviour
    {
        public abstract event Action OnShowEvent;
        public abstract event Action OnShowEndEvent;
        public abstract event Action OnHideEvent;

        public abstract void Initialize();
        public abstract void ShowView();
        public abstract void HideView();

        public void ToggleView(bool value)
        {
            if (value)
            {
                ShowView();
            }
            else
            {
                HideView();
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Initialize")]
        private void Initialize_EditorOnly() => Initialize();

        [ContextMenu("Show View")]
        private void ShowView_EditorOnly() => ShowView();

        [ContextMenu("Hide View")]
        private void HideView_EditorOnly() => HideView();
#endif
    }

    public abstract class Controller<TView> : Controller
    where TView : View
    {
        [field: Required, SerializeField]
        protected TView View { get; private set; }

        public override event Action OnShowEvent
        {
            add => View.OnShowEvent += value;
            remove => View.OnShowEvent -= value;
        }
        public override event Action OnShowEndEvent
        {
            add => View.OnShowEndEvent += value;
            remove => View.OnShowEndEvent -= value;
        }
        public override event Action OnHideEvent
        {
            add => View.OnHideEvent += value;
            remove => View.OnHideEvent -= value;
        }

        public override void Initialize()
        {
            View.OnInitialize();
        }

        public override void ShowView()
        {
            View.OnShow();
        }

        public override void HideView()
        {
            View.OnHide();
        }
    }

    public abstract class Controller<TModel, TView> : Controller<TView>
    where TModel : Model
    where TView : View<TModel>
    {
        [field: Required, SerializeField]
        protected TModel Model { get; private set; }
    }
}