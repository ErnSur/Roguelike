using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LDF.EventSystem
{
    public static class EventManager
    {
        public delegate void EventDelegate<T>(T e) where T : Event;
        internal delegate void InternalDelegate(Event e);

        private static readonly Repository _repository;

        static EventManager()
        {
            _repository = new Repository();
        }

        public static bool AddListener<T>(EventDelegate<T> action) where T : Event
        {
            if (TryAddListener(action, out var newDelegate))
            {
                AddOrUpdateMultiUseStore();

                return true;
            }

            return false;

            void AddOrUpdateMultiUseStore()
            {
                Type targetType = typeof(T);

                if (_repository.MultiUseDelegates.ContainsKey(targetType))
                {
                    _repository.MultiUseDelegates[targetType] += newDelegate;
                }
                else
                {
                    _repository.MultiUseDelegates.Add(targetType, newDelegate);
                }
            }
        }

        public static bool AddOneTimeListener<T>(EventDelegate<T> action) where T : Event
        {
            if (TryAddListener(action, out var newDelegate))
            {
                var targetType = typeof(T);
                _repository.SingleUseDelegates[targetType] = newDelegate;

                return true;
            }

            return false;
        }

        public static void RemoveListener<T>(EventDelegate<T> action) where T : Event
        {
            var targetType = typeof(T);

            if (_repository.RegisteredDelegates.TryGetValue(action, out var registredDelgate))
            {

                if (_repository.SingleUseDelegates.ContainsKey(targetType))
                {
                    RemoveListenerFromStore(_repository.SingleUseDelegates, registredDelgate, targetType);
                }
                else if (_repository.MultiUseDelegates.TryGetValue(targetType, out var multiDelegate))
                {
                    multiDelegate -= registredDelgate;
                    RemoveListenerFromStore(_repository.MultiUseDelegates, registredDelgate, targetType);
                }

                if (_repository.MultiUseDelegates == null)
                {
                    _repository.MultiUseDelegates.Remove(targetType);
                }
            }
        }

        public static void RemoveAllListenersOfType<T>()
        {
            var targetType = typeof(T);

            if (_repository.SingleUseDelegates.TryGetValue(targetType, out var oneTimeDel))
            {
                RemoveListenerFromStore(_repository.SingleUseDelegates, oneTimeDel, targetType);
            }

            if (_repository.MultiUseDelegates.TryGetValue(targetType, out var currentDelegate))
            {
                RemoveListenerFromStore(_repository.MultiUseDelegates, currentDelegate, targetType);
            }
        }

        public static void RemoveAllListeners()
        {
            _repository.MultiUseDelegates.Clear();
            _repository.SingleUseDelegates.Clear();
            _repository.RegisteredDelegates.Clear();
        }

        public static bool HasListener<T>(EventDelegate<T> del) where T : Event
        {
            return _repository.RegisteredDelegates.ContainsKey(del);
        }

        public static void TriggerEvent(Event payload)
        {
            var targetType = payload.GetType();

            if (!TryInvokeSingleUseDelegate(payload, targetType))
            {
                TryInvokeMultiUseDelegate(payload, targetType);
            }
        }

        private static bool TryAddListener<T>(EventDelegate<T> action, out InternalDelegate newDelegate) where T : Event
        {
            if (HasListener(action))
            {
                newDelegate = null;

                return false;
            }

            newDelegate = (e) => action.Invoke((T)e);
            _repository.RegisteredDelegates.Add(action, newDelegate);

            return true;
        }

        private static bool TryInvokeSingleUseDelegate(Event payload, Type targetType)
        {
            return TryInvokeDelegateFromStore(_repository.SingleUseDelegates, targetType, payload, true);
        }

        private static bool TryInvokeMultiUseDelegate(Event payload, Type targetType)
        {
            return TryInvokeDelegateFromStore(_repository.MultiUseDelegates, targetType, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool TryInvokeDelegateFromStore(Dictionary<Type, InternalDelegate> store, Type targetType, Event payload, bool removeOnUse = false)
        {
            if (store.TryGetValue(targetType, out var currentDelegate))
            {
                if (removeOnUse)
                {
                    RemoveListenerFromStore(store, currentDelegate, targetType);
                }

                currentDelegate.Invoke(payload);

                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void RemoveListenerFromStore(Dictionary<Type, InternalDelegate> store, InternalDelegate targetDelegate, Type targetType)
        {
            store.Remove(targetType);

            var registredInTargets = _repository.RegisteredDelegates
                                                .Where(kvp => kvp.Value == targetDelegate)
                                                .Select(kvp => kvp.Value)
                                                .ToArray();

            foreach (var target in registredInTargets)
            {
                _repository.RegisteredDelegates.Remove(target);
            }
        }

        private class Repository
        {
            public readonly Dictionary<Type, InternalDelegate> MultiUseDelegates = new Dictionary<Type, InternalDelegate>();
            public readonly Dictionary<Type, InternalDelegate> SingleUseDelegates = new Dictionary<Type, InternalDelegate>();
            public readonly Dictionary<Delegate, InternalDelegate> RegisteredDelegates = new Dictionary<Delegate, InternalDelegate>();
        }
    }
}