#nullable enable
using Projectiles.Logic;
using UnityEngine;

namespace Projectiles
{
    public class DefaultProjectileController : IProjectileController
    {
        public Vector3 StartingPosition { get; }
        public IMovementBehavior Movement { get; }

        public DefaultProjectileController(Vector3 startingPosition, Vector3 direction)
        {
            StartingPosition = startingPosition;
            Movement = new DirectMovementBehavior(direction);
        }
    }
}