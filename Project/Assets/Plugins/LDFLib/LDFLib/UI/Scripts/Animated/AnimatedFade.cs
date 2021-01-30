using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LDF.Utility;

namespace LDF.UserInterface.Animations
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AnimatedFade : Animated
    {
        public Status CurrnetStatus { get; private set; }

        [Required, SerializeField]
        private CanvasGroup _group;

        [SerializeField, MinMax]
        [Tooltip("x = HideAlpha, y = ShowAlpha")]
        private Vector2 _fadeAlphaRange = new Vector2(0, 1);

        [SerializeField]
        private float _fadeDuration = 0.75f;

        [SerializeField]
        private bool _toggleActive = true;

        [SerializeField]
        private bool _startHidden = true;

        private Coroutine _currentCoroutine;

        private bool _inited;

        public override bool InProgress
        {
            get => CurrnetStatus == Status.DuringShow
                || CurrnetStatus == Status.DuringHide;
        }

        private float _ShowAlpha => _fadeAlphaRange.y;

        private float _HideAlpha => _fadeAlphaRange.x;

        public override bool IsOrWillBeVisible => CurrnetStatus == Status.Visible || CurrnetStatus == Status.DuringShow;

        private bool _IsOrWillBeHidden => CurrnetStatus == Status.Hidden || CurrnetStatus == Status.DuringHide;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if (_inited)
            {
                return;
            }

            if (_startHidden)
            {
                _group.alpha = _HideAlpha;
                CurrnetStatus = Status.Hidden;

                if (_toggleActive)
                {
                    gameObject.SetActive(false);
                }
            }

            _inited = true;
        }

        public override void Show(Action onEnd)
        {
            Init();
            if (IsOrWillBeVisible)
            {
                onEnd?.Invoke();
                return;
            }

            gameObject.SetActive(true);
            StartFade(Fade.In, onEnd);
        }

        public IEnumerator ShowCoroutine()
        {
            Init();
            if (IsOrWillBeVisible)
            {
                yield break;
            }

            gameObject.SetActive(true);
            yield return FadeCanvasGroup(Fade.In, null);
        }

        public override void Hide(Action onEnd)
        {
            if (_IsOrWillBeHidden)
            {
                onEnd?.Invoke();
                return;
            }

            StartFade(Fade.Out, onEnd);
        }

        public override void Toggle(Action onEnd)
        {
            if (_IsOrWillBeHidden)
            {
                Show(onEnd);
            }
            else
            {
                Hide(onEnd);
            }
        }

        private void StartFade(Fade fade, Action onEnd)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            CurrnetStatus = fade == Fade.In ? Status.DuringShow : Status.DuringHide;

            _currentCoroutine = StartCoroutine(FadeCanvasGroup(fade, onEnd));
        }

        private IEnumerator FadeCanvasGroup(Fade fade, Action onEnd)
        {
            OnBeforeFade();

            var endAlpha = GetFadeEndAlpha(fade);

            foreach (var step in LerpSteps(_group.alpha, endAlpha, _fadeDuration))
            {
                _group.alpha = step;
                yield return null;
            }

            OnAfterFade(fade);
            onEnd?.Invoke();
        }

        private float GetFadeEndAlpha(Fade fade)
        {
            switch (fade)
            {
                case Fade.Out: return _HideAlpha;
                case Fade.In: return _ShowAlpha;
                default: throw new NotSupportedException();
            }
        }

        private void OnBeforeFade()
        {
            _group.interactable = false;
        }

        private void OnAfterFade(Fade fade)
        {
            if (_toggleActive && fade == Fade.Out)
            {
                gameObject.SetActive(false);
            }

            CurrnetStatus = fade == Fade.In ? Status.Visible : Status.Hidden;
            _currentCoroutine = null;
            _group.interactable = true;
        }

        private IEnumerable<float> LerpSteps(float start, float end, float duration)
        {
            for (var t = 0f; t < 1f; t += Time.deltaTime / duration)
            {
                yield return Mathf.Lerp(start, end, t);
            }
            yield return end;
        }

        public enum Status { Visible, Hidden, DuringShow, DuringHide }
        private enum Fade { In, Out }
    }
}
