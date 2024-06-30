#nullable enable
using Projectiles.Logic;
using UnityEngine;

namespace Projectiles
{
    public interface IProjectileController
    {
        Vector3 StartingPosition { get; }
        IMovementBehavior Movement { get; }
    }
}