#nullable enable
using System;
using System.Collections;
using UnityEngine;

namespace Projectiles.Logic
{
    /// <summary>
    /// Перемещение по прямой траектории
    /// </summary>
    public class DirectMovementBehavior : IMovementBehavior
    {
        public bool OnMove => !_stopMovement;
        
        private readonly Vector3 _direction;

        private Func<Collider, bool>? _onCollisionWithOtherObject;
        private bool _stopMovement;
        private float _launchVelocity;

        public DirectMovementBehavior(Vector3 direction)
        {
            _direction = direction;
        }

        public IEnumerator Move(Transform projectile, Func<Collider, bool> onCollisionWithOtherObject, Action onComplete)
        {
            _onCollisionWithOtherObject = onCollisionWithOtherObject;
            
            while (true)
            {
                projectile.Translate(_direction * _launchVelocity * Time.deltaTime, Space.World);
                yield return null;

                if (_stopMovement)
                {
                    break;
                }
            }

            onComplete?.Invoke();
        }

        public void SetVelocity(float velocity)
        {
            _launchVelocity = velocity;
        }

        public void OnTriggerEnter(Collider collider)
        {
            if (collider.transform.GetComponent<IProjectile>() != null)
            {
                return;
            }

            var needStopMovement = _onCollisionWithOtherObject?.Invoke(collider);
            _stopMovement = needStopMovement != null && needStopMovement.Value;
        }

        public void Dispose()
        {
            _onCollisionWithOtherObject = null;
        }
    }
}