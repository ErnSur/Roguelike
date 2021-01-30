using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

namespace LDF.UserInterface
{
    [RequireComponent(typeof(RectTransform))]
    public class OutsideClickTrigger : MonoBehaviour
    {
        public static bool IsFunctionalityDisabled;

        public UnityEvent OnClickOutside;

        [SerializeField]
        private RectTransform[] _safeRects;

        [SerializeField]
        private Button[] _buttonsToTrigger;

        [SerializeField]
        private Toggle[] _togglesToTrigger;

        private ISubmitHandler[] _elementsToTrigger;

        private void Awake()
        {
            Input.simulateMouseWithTouches = true;
            _elementsToTrigger = _buttonsToTrigger.Concat<ISubmitHandler>(_togglesToTrigger).ToArray();
        }

        private void Update()
        {
            TriggerIfClickedOutside();
        }

        private void TriggerIfClickedOutside()
        {
            if (Input.GetMouseButtonUp(0) && !IsPointerInsideSafeRect(Input.mousePosition) && !IsFunctionalityDisabled)
            {
                TriggerSubmitHandlers();
                OnClickOutside.Invoke();
            }
        }

        private void TriggerSubmitHandlers()
        {
            var eventData = new BaseEventData(EventSystem.current);

            foreach (var element in _elementsToTrigger)
            {
                element.OnSubmit(eventData);
            }
        }

        private bool IsPointerInsideSafeRect(Vector2 pointerPos)
        {
            return _safeRects.Any(rect => RectTransformUtility.RectangleContainsScreenPoint(rect, pointerPos));
        }
    }
}