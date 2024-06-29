#nullable enable
using UnityEngine;

namespace Data
{
    public readonly struct SnakePartData
    {
        public readonly Vector3 StartingPosition;
        public readonly float Speed;
        public readonly float AngularSpeed;
        public readonly float StoppingDistance;
        public readonly float Acceleration;

        public SnakePartData(Vector3 startingPosition, float speed, float angularSpeed, float stoppingDistance, float acceleration)
        {
            StartingPosition = startingPosition;
            Speed = speed;
            AngularSpeed = angularSpeed;
            StoppingDistance = stoppingDistance;
            Acceleration = acceleration;
        }
    }
}