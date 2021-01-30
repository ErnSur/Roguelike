using System;
using System.Collections;
using UnityEngine;
using LDF.UserInterface.Animations;
using LDF.Utility;

namespace LDF.UserInterface
{
    [RequireComponent(typeof(AnimatedFade))]
    public class CanvasGroupFlasher : MonoBehaviour
    {
        [Required, SerializeField]
        private AnimatedFade _fadeAnimator;

        private Coroutine _currentCoroutine;

        public bool InProgress => _fadeAnimator.CurrnetStatus != AnimatedFade.Status.Hidden;

        public void StartFlash(Action onMidpoint, Action onEnd = null)
        {
            StartFlash(new FlashEvents(onMidpoint, onEnd));
        }

        public void StartFlash(IEnumerator onMidpoint, Action onEnd = null)
        {
            StartFlash(new FlashEvents(onMidpoint, onEnd));
        }

        private void StartFlash(FlashEvents events)
        {
            if (_currentCoroutine != null)
            {
                return;
            }

            gameObject.SetActive(true);

            _currentCoroutine = StartCoroutine(Flash(events));
        }

        private IEnumerator Flash(FlashEvents events)
        {
            yield return FadeIn();

            if(events != null)
            {
                events.OnMidpointAction?.Invoke();
                yield return events.OnMidpointCoroutine;
            }

            FadeOut(events);
        }

        private IEnumerator FadeIn()
        {
            _fadeAnimator.Show();

            while (_fadeAnimator.InProgress)
            {
                yield return null;
            }
        }

        private void FadeOut(FlashEvents events)
        {
            _fadeAnimator.Hide(OnFadeEnd);

            void OnFadeEnd()
            {
                _currentCoroutine = null;
                events?.OnEnd?.Invoke();
            }
        }

        private class FlashEvents
        {
            public readonly IEnumerator OnMidpointCoroutine;
            public readonly Action OnMidpointAction;

            public readonly Action OnEnd;

            public FlashEvents(IEnumerator onMidpointCoroutine, Action onEnd = null)
            {
                OnMidpointCoroutine = onMidpointCoroutine;
                OnEnd = onEnd;
            }

            public FlashEvents(Action onMidpointAction, Action onEnd = null)
            {
                OnMidpointAction = onMidpointAction;
                OnEnd = onEnd;
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Start Flash")]
        public void StartFlash() => StartFlash(null);
#endif
    }
}
