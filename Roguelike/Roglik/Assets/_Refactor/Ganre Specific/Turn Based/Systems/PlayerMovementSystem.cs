using System;
using LDF.Structures;
using LDF.Utils;
using UnityEngine;

namespace LDF.Systems
{    
    public interface IPlayerMovementSystemInput
    {
        float PlayerSpeed { get; }
        Vector2Int PlayerGridPos { get; set; }
        Transform PlayerTransform { get; }
        bool CanMoveInDirection(Direction direction);
    }

    public class PlayerMovementSystem : TurnSystemBehaviour<IPlayerMovementSystemInput>
    {
        public event Action OnPositionChange;
        public event Action<Direction> OnDirectionChange;

        private readonly Direction[] _directions = (Direction[]) Enum.GetValues(typeof(Direction));

        protected override void OnTurnUpdate(Turn turn)
        {
            if (turn != Turn.Player)
                return;

            ChangePlayerPositionOnInput();
            MovePlayerTransform();
        }

        private void MovePlayerTransform()
        {
            input.PlayerTransform.position =
                Vector3.MoveTowards
                (
                    input.PlayerTransform.position,
                    input.PlayerGridPos.ToVector3(),
                    Time.deltaTime * input.PlayerSpeed
                );
        }

        private void ChangePlayerPositionOnInput()
        {
            foreach (var direction in _directions)
            {
                if (!CanChangeDirection(direction))
                    continue;

                ChangePlayerDirection(direction);

                if (!input.CanMoveInDirection(direction))
                    continue;

                ChangePlayerPosition(direction.AsVector2Int());
                NextTurn(GetLogMessage(direction));
            }
        }

        private void ChangePlayerDirection(Direction direction)
        {
            OnDirectionChange?.Invoke(direction);
        }

        private void ChangePlayerPosition(Vector2Int direction)
        {
            input.PlayerGridPos += direction;
            OnPositionChange?.Invoke();
        }

        private bool CanChangeDirection(Direction direction)
        {
            // floating point numbers comparison
            return Input.GetKey(direction.AsKeyCode())
                   && input.PlayerTransform.position == input.PlayerGridPos.ToVector3();
        }

        private string GetLogMessage(Direction direction)
        {
            return $"I moved to the {direction.ToString().ToLower()}.";
        }
    }
}