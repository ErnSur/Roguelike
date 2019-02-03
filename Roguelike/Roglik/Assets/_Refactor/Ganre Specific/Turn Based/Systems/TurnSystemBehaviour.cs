using System;

namespace LDF.Systems
{
    public abstract class TurnSystemBehaviour : SystemBehaviour
    {
        public event TurnAction OnNextTurn;

        private static Turn _currentTurn;

        protected virtual void OnTurnUpdate(Turn turn)
        {
        }

        protected void NextTurn(string logMessage)
        {
            //LogMessage to GameLog, message log could be class with logType to filter log messages
            ChangeTurn();
            OnNextTurn?.Invoke(_currentTurn);
        }

        private void ChangeTurn()
        {
            _currentTurn = _currentTurn == Turn.Enemy ? Turn.Player : Turn.Enemy;
        }

        #region Definitions
        
        public delegate void TurnAction(Turn turn);

        public enum Turn
        {
            Player,
            Enemy
        }

        #endregion
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