using System;
using System.Threading.Tasks;
using LDF.Structures;
using LDF.Utils;
using UnityEngine;

namespace LDF.Systems
{    
    public interface IPlayerMovement_SInput
    {
        float PlayerSpeed { get; }
        Vector2Int PlayerGridPos { get; set; }
        Vector3 PlayerTransformPosition { get; set; }
        bool CanMoveInDirection(Vector2Int direction);
    }

    public class PlayerMovement_S : TurnSystemBehaviour<IPlayerMovement_SInput>
    {
        public event Action OnPositionChange;
        public event Action<Direction> OnDirectionChange;

        private readonly Direction[] _directions = (Direction[]) Enum.GetValues(typeof(Direction));

        //public delegate void MovePlayerTransformAction(Vector3 direction, int distance, Action onEnd);

        //private MovePlayerTransformAction _movePlayerTransform;

        protected override void OnTurnUpdate(Turn turn)
        {
            MovePlayerTransform();
            
            if (turn != Turn.Player)
                return;

            ChangePlayerPositionOnInput();
        }

        private void MovePlayerTransform()
        {
            input.PlayerTransformPosition =
                Vector3.MoveTowards
                (
                    input.PlayerTransformPosition,
                    input.PlayerGridPos.ToVector3(),
                    Time.deltaTime * input.PlayerSpeed
                );
        }

        private void ChangePlayerPositionOnInput()
        {
            foreach (var direction in _directions)
            {
                if (!CanChangeDirection(direction)) // Key Press
                    continue;

                ChangePlayerDirection(direction);

                if (!input.CanMoveInDirection(direction.AsVector2Int()))
                    continue;

                ChangePlayerGridPosition(direction.AsVector2Int());
                NextTurn(GetLogMessage(direction));
            }
        }

        private void ChangePlayerPositionOnInput2()
        {
            foreach (var direction in _directions)
            {
                if (Input.GetKey(direction.AsKeyCode()))
                {
                    OnDirectionChange?.Invoke(direction);

                    if (input.CanMoveInDirection(direction.AsVector2Int()))
                    {
                        //ChangePlayerPosition( () => NextTurn(GetLogMessage(direction)) );
                    }
                }
            }
        }
        
        private void ChangePlayerDirection(Direction direction)
        {
            OnDirectionChange?.Invoke(direction);
        }

        private void ChangePlayerGridPosition(Vector2Int direction)
        {
            input.PlayerGridPos += direction;
            OnPositionChange?.Invoke();
        }

        private bool CanChangeDirection(Direction direction)
        {
            // floating point numbers comparison
            return Input.GetKey(direction.AsKeyCode())
                   && input.PlayerTransformPosition == input.PlayerGridPos.ToVector3();
        }

        private static string GetLogMessage(Direction direction)
        {
            return $"I moved {direction.ToString().ToLower()}.";
        }
    }
}