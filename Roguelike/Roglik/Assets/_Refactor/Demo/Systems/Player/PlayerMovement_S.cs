using System;
using LDF.Structures;
using LDF.Utility;
using UnityEngine;

namespace LDF.Systems
{
    public interface IPlayerMovement_SInput
    {
        float PlayerSpeed { get; }
        Vector2Int PlayerGridPos { get; set; }
        Transform PlayerTransform { get; }
    }

    public class PlayerMovement_S : TurnSystemBehaviour<IPlayerMovement_SInput>, IMovable
    {
        public event Action OnPositionChange;
        public event Action<Direction> OnDirectionChange;

        private readonly Direction[] _directions = DirectionExtensions.Get4Directions();

        private Direction? _nextMove;

        protected override void OnTurnUpdate(Turn turn)
        {
            MovePlayerTransform();

            if (turn != Turn.Player)
                return;

            ChangePlayerPositionOnInput();
        }

        private void MovePlayerTransform()
        {
            if (_nextMove != null &&
                MoveInDirection(input.PlayerTransform, input.PlayerGridPos.ToVector3(), input.PlayerSpeed))
            {
                _nextMove = null;
            }
        }

        private void ChangePlayerPositionOnInput()
        {
            foreach (var direction in _directions)
            {
                if (!CanChangeDirection(direction)) // Key Press
                    continue;

                ChangePlayerDirection(direction);

                if (!CanMoveInDirection(direction.AsVector2Int()))
                    continue;

                ChangePlayerGridPosition(direction);
            }

            if (Input.GetButtonDown("Cancel"))
            {
                EndTurn("...");
            }
        }
        
        private bool CanMoveInDirection(Vector2Int direction)
        {
            return ScenePathfinding.IsWalkable(input.PlayerGridPos + direction);
        }

        private void ChangePlayerDirection(Direction direction)
        {
            OnDirectionChange?.Invoke(direction);
        }

        private void ChangePlayerGridPosition(Direction direction)
        {
            input.PlayerGridPos += direction.AsVector2Int();
            _nextMove = direction;
            EndTurn(GetLogMessage(_nextMove.Value));

            OnPositionChange?.Invoke();
        }

        private bool CanChangeDirection(Direction direction)
        {
            return Input.GetKey(direction.AsKeyCode()) && TransformIsOnCorrectPosition();
        }

        private static string GetLogMessage(Direction direction)
        {
            //todo: Create Log Class that has Color filed that adds <Color> prefix and sufix based on that field
            return $"<color=#E3E3E3> Me: I moved {direction.ToString().ToLower()}.</color>";
        }

        // floating point numbers comparison
        private bool TransformIsOnCorrectPosition()
        {
            return input.PlayerTransform.position == input.PlayerGridPos.ToVector3();
        }

        public bool MoveInDirection(Transform subject, Vector3 destination, float speed)
        {
            return this.DefaultImplementation(subject, destination, speed);
        }
    }
}