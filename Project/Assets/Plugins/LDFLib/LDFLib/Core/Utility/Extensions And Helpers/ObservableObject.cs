using System;
using System.Collections.Generic;
using UnityEngine;

namespace LDF.Core
{
    [Serializable]
    public class ObservableObject<T>
    {
        private readonly List<Action<T>> _subscribers = new List<Action<T>>();

        [SerializeField]
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                if (!_notifyAlways) //  #hack: pre-release - force notify all subscribers
                {
                    if (EqualityComparer<T>.Default.Equals(_value, value))
                    {
                            return;
                    }
                }

                _value = value;

                NotifySubscribers(value);
            }
        }

        // #hack: pre-release - force notify all subscribers
        private bool _notifyAlways;

        public ObservableObject(bool notifyAlways)
        {
            _notifyAlways = notifyAlways;
        }
        //  #endhack

        public ObservableObject()
        {
        }

        public ObservableObject(T value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ObservableObject<T>;
            return other != null && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static explicit operator T(ObservableObject<T> obj)
        {
            return obj.Value;
        }

        public static bool operator ==(ObservableObject<T> left, T right)
        {
            return left.Value.Equals(right);
        }

        public static bool operator !=(ObservableObject<T> left, T right)
        {
            return !left.Value.Equals(right);
        }

        public void Subscribe(Action<T> callback, bool notifyOnSubscribe = true)
        {
            _subscribers.Add(callback);

            if(notifyOnSubscribe)
            {
                NotifySubscriberWithCurrentValue(callback);
            }
        }

        public void Unsubscribe(Action<T> callback)
        {
            _subscribers.Remove(callback);
        }

        public void NotifySubscribers(T value)
        {
            for (int i = _subscribers.Count - 1; i > -1 ; --i)
            {
                if (_subscribers[i] == null)
                {
                    Unsubscribe(_subscribers[i]);

                    continue;
                }

                _subscribers[i].Invoke(value);
            }
        }

        public void NotifySubscriberWithCurrentValue(Action<T> callback)
        {
            callback?.Invoke(Value);
        }
    }
}