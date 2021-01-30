using UnityEngine;

namespace LDF.Systems
{
    public interface IMovable
    {
        bool MoveInDirection(Transform subject, Vector3 destination, float speed);
    }
    
    public static partial class GlobalFunctions
    {

        public static bool DefaultImplementation(this IMovable f, Transform subject, Vector3 destination, float speed)
        {
            var newPosition = Vector3.MoveTowards
            (
                subject.position,
                destination,
                Time.deltaTime * speed
            );

            subject.position = newPosition;

            return newPosition == destination;
        }
    }
}