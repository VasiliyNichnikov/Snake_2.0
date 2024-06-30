#nullable enable
using System;
using System.Collections;
using UnityEngine;

namespace Projectiles.Logic
{
    public interface IMovementBehavior : IDisposable
    {
        bool OnMove { get; }
        
        IEnumerator Move(Transform projectile, Func<Collider, bool> onCollisionWithOtherObject, Action onComplete);

        void SetVelocity(float velocity);
        
        void OnTriggerEnter(Collider collider);
    }
}