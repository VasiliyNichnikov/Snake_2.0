#nullable enable
using System;
using Pools;
using UnityEngine;

namespace Projectiles
{
    public interface IProjectile : IPoolObject
    {
        ProjectileType Type { get; }

        void Init(IProjectileController controller);
        
        void ToRun(Func<Collider, bool> onCollisionWithOtherObject);
    }
}