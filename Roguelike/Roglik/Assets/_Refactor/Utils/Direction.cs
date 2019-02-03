using System;
using LDF.UserInput;
using UnityEngine;

namespace LDF.Utils
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public static class DirectionExtensions
    {
        public static Vector3 GetVector3(this Direction d)
        {
            switch (d)
            {
                case Direction.Left:  return Vector3.left;
                case Direction.Right: return Vector3.right;
                case Direction.Up:    return Vector3.up;
                case Direction.Down:  return Vector3.down;
                default: throw new ArgumentOutOfRangeException(nameof(d), d, null);
            }
        }
        
        public static Vector2Int GetVector2Int(this Direction d)
        {
            switch (d)
            {
                case Direction.Left:  return Vector2Int.left;
                case Direction.Right: return Vector2Int.right;
                case Direction.Up:    return Vector2Int.up;
                case Direction.Down:  return Vector2Int.down;
                default: throw new ArgumentOutOfRangeException(nameof(d), d, null);
            }
        }
        
        public static KeyCode GetKeyCode(this Direction d)
        {
            switch (d)
            {
                case Direction.Left:  return UserControls.MoveLeft;
                case Direction.Right: return UserControls.MoveRight;
                case Direction.Up:    return UserControls.MoveUp;
                case Direction.Down:  return UserControls.MoveDown;
                default: throw new ArgumentOutOfRangeException(nameof(d), d, null);
            }
        }
        
        public static Direction[] GetValues(this Direction d)
        {
            var enumType = d.GetType();

            return (Direction[])Enum.GetValues(enumType);
        } 
    }
}