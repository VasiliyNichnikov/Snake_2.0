#nullable enable
using System;
using System.Collections;
using Data;
using UnityEngine;

namespace Projectiles
{
    public class DefaultProjectile : MonoBehaviour, IProjectile
    {
        public ProjectileType Type => _data.Type;

        private IProjectileController _controller = null!;
        private DefaultProjectileData _data;

        private IEnumerator? _runningMovement;
        private ProjectilesManager _projectilesManager = null!;

        public void InitFactory(DefaultProjectileData data, ProjectilesManager projectilesManager)
        {
            _data = data;
            _projectilesManager = projectilesManager;
        }
        
        public void Init(IProjectileController controller)
        {
            _controller = controller;
            transform.position = controller.StartingPosition;
            _controller.Movement.SetVelocity(_data.Speed);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void ToRun(Func<Collider, bool> onCollisionWithOtherObject)
        {
            TryStopCurrentAnimation();
            _runningMovement = _controller.Movement.Move(transform, onCollisionWithOtherObject, OnCompleteMovement);
            StartCoroutine(_runningMovement);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void TryStopCurrentAnimation()
        {
            if (_runningMovement != null)
            {
                StopCoroutine(_runningMovement);
                _runningMovement = null;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_runningMovement == null || !_controller.Movement.OnMove)
            {
                return;
            }


            _controller.Movement.OnTriggerEnter(other);
        }
        
        private void OnCompleteMovement()
        {
            _projectilesManager.RemoveProjectile(this);
        }
    }
}