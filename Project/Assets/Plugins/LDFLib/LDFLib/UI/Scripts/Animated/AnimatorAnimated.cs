using System;
using UnityEngine;

namespace LDF.UserInterface.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorAnimated : Animated
    {
        private const string _startParameter = "Show";
        private const string _startingParameter = "Starting";
        private const string _hideParameter = "Hide";
        [SerializeField]
        private float _playbackSpeed = 1f;

        // members 
        private bool _hidden = true;
        private bool _starting = true;
        private Animator _animator;

        // Unity methods
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.speed = _playbackSpeed;
        }

        public override bool IsOrWillBeVisible => base.IsOrWillBeVisible;

        public override void Show(Action onEnd)
        {
            gameObject.SetActive(true);

            _hidden = false;
            InProgress = true;
            _animator.SetBool(_startingParameter, _starting);
            _animator.SetTrigger(_startParameter);
        }

        public override void Hide(Action onEnd)
        {
            _hidden = true;
            InProgress = true;
            _animator.SetTrigger(_hideParameter);
        }

        public override void Toggle(Action onEnd)
        {
            if (_hidden)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void ShowEnd()
        {
            if (!_hidden)
            {
                _starting = false;
                InProgress = false;
            }
        }

        public void HideEnd()
        {
            if (_hidden)
            {
                gameObject.SetActive(false);
                InProgress = false;
            }
        }
    }
}