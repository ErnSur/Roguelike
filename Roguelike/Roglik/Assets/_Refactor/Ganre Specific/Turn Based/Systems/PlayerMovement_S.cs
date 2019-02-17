using System;
using System.Threading.Tasks;
using LDF.Structures;
using LDF.Utils;
using UnityEngine;

namespace LDF.Systems
{
    public static partial class GlobalFunctions
    {
        private static MoveInDirection _moveInDirection;
        
        public static MoveInDirection MoveInDirection() => _moveInDirection.Default();
        public static MoveInDirection Default(this MoveInDirection f)
        {
            return DefaultImplementation;
            
            bool DefaultImplementation(Transform tr, Vector3 destination, float speed)
            {
                var position = tr.position;
                
                position = Vector3.MoveTowards
                (
                    position,
                    destination,
                    Time.deltaTime * speed
                );
                
                tr.position = position;

                return position == destination;
            }
        }
    }
}

namespace LDF.Systems
{    
    public interface IPlayerMovement_SInput
    {
        float PlayerSpeed { get; }
        Vector2Int PlayerGridPos { get; set; }
        Transform PlayerTransform { get; }
        bool CanMoveInDirection(Vector2Int direction);
    }

    public delegate bool MoveInDirection(Transform subject, Vector3 destination, float speed);
    
    public class PlayerMovement_S : TurnSystemBehaviour<IPlayerMovement_SInput>
    {
        public event Action OnPositionChange;
        public event Action<Direction> OnDirectionChange;

        private readonly Direction[] _directions = (Direction[]) Enum.GetValues(typeof(Direction));

        private Direction? _nextMove;

        private MoveInDirection _moveInDir = GlobalFunctions.MoveInDirection();

        protected override void OnTurnUpdate(Turn turn)
        {
            if (turn != Turn.Player)
                return;
            
            MovePlayerTransform();

            ChangePlayerPositionOnInput();
        }

        private void MovePlayerTransform()
        {
            if (_nextMove != null && _moveInDir(input.PlayerTransform, input.PlayerGridPos.ToVector3(), input.PlayerSpeed))
            {
                NextTurn(GetLogMessage(_nextMove.Value));
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

                if (!input.CanMoveInDirection(direction.AsVector2Int()))
                    continue;

                ChangePlayerGridPosition(direction);
            }
        }
        
        private void ChangePlayerDirection(Direction direction)
        {
            OnDirectionChange?.Invoke(direction);
        }

        private void ChangePlayerGridPosition(Direction direction)
        {
            input.PlayerGridPos += direction.AsVector2Int();
            _nextMove = direction;
            OnPositionChange?.Invoke();
        }

        private bool CanChangeDirection(Direction direction)
        {
            // floating point numbers comparison
            return Input.GetKey(direction.AsKeyCode()) && TransformIsOnCorrectPosition();
        }

        private static string GetLogMessage(Direction direction)
        {
            return $"I moved {direction.ToString().ToLower()}.";
        }

        private bool TransformIsOnCorrectPosition()
        {
            return input.PlayerTransform.position == input.PlayerGridPos.ToVector3();
        }
    }
}