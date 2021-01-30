using System;
using System.Linq;
using LDF.Structures;
using LDF.UserInput;
using UnityEngine;

namespace LDF.Utility
{
    public static class DirectionExtensions
    {
        public static Vector3 AsVector3(this Direction d)
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
        
        public static Vector3[] AsVector3(this Direction[] directions)
        {
            return directions.Select(AsVector3).ToArray();
        }
        
        public static Vector2Int AsVector2Int(this Direction d)
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
        
        public static Vector2Int[] AsVector2Int(this Direction[] directions)
        {
            return directions.Select(AsVector2Int).ToArray();
        }
        
        public static KeyCode AsKeyCode(this Direction d)
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
        
        public static KeyCode[] AsKeyCode(this Direction[] directions)
        {
            return directions.Select(AsKeyCode).ToArray();
        }

        public static Direction[] Get4Directions()
        {
            return (Direction[]) Enum.GetValues(typeof(Direction));
        }
    }
}