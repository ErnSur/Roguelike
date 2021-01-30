//#define DEBUG_LOG
using System;
using System.Diagnostics;
using UnityEngine;

namespace LDF.Systems
{
    public abstract class TurnSystemBehaviour : SystemBehaviour
    {
        private static event TurnAction _onTurnStartEvent;
        public static event Action<string> LogMessageSent;

        private static Turn _currentTurn;

        protected virtual void OnEnable() => _onTurnStartEvent += OnTurnStart;
        protected virtual void OnDisable() => _onTurnStartEvent -= OnTurnStart;

        private void Update()
        {
            OnTurnUpdate(_currentTurn);
        }

        protected virtual void OnTurnStart(Turn turn)
        {
        }

        protected virtual void OnTurnUpdate(Turn turn)
        {
        }

        protected void EndTurn(string logMessage)
        {
            LogMessageSent?.Invoke(logMessage);
            ConsoleLog(logMessage);
            ChangeTurn();
        }

        private static void ChangeTurn()
        {
            _currentTurn = _currentTurn == Turn.Enemy ? Turn.Player : Turn.Enemy;
            _onTurnStartEvent?.Invoke(_currentTurn);
        }

        #region Definitions

        public delegate void TurnAction(Turn turn);

        public enum Turn
        {
            Player,
            Enemy
        }

        public class GameLog
        {
            private string _source, _message;

            public override string ToString()
            {
                return $"{_source}: {_message}";
            }
        }

        #endregion

        [Conditional("DEBUG_LOG")]
        private void ConsoleLog(string mesage)
        {
            UnityEngine.Debug.Log($"<color=green>[Game Log]</color> {mesage}", this);
        }
    }

    public abstract class TurnSystemBehaviour<T> : TurnSystemBehaviour
    {
        protected T input { get; private set; }

        protected override void Awake()
        {
            GetSystemInput();
            Init();
        }

        private void GetSystemInput()
        {
            input = GetComponent<T>();

            if (input == null)
            {
                throw new NullReferenceException(
                    $"{name} was not Initialized, couldn't find input of type {typeof(T).Name}");
            }
        }
    }
}