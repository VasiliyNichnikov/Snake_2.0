#nullable enable
using System;
using System.Collections;
using Entities;
using UnityEngine;
using Utils;

namespace Enemies
{
    public class MeleeEnemyAttack : IDisposable
    {
        public bool CanAttackPlayer => _enemyMovement.IsPathCompleted() && _enemyMovement.GetDistanceToPlayer() <= _minimumDistanceToAttackPlayer;
        public bool IsWeaponReloaded { get; private set; } = true;
        
        public event Action? OnTakeDamage;
        
        private readonly Transform _enemyTransform;
        private readonly float _recharge;
        private readonly float _minimumDistanceToAttackPlayer;
        private readonly EnemyMovement _enemyMovement;
        private readonly Timer _timer;
        private readonly LayerMask _playerLayerMask;
        private readonly int _damage;
        private readonly EnemyAnimator _animator;
        private readonly float _attackAnimationTime;
        private readonly MonoBehaviour _enemyMono;

        private IEnumerator? _attack;

        public MeleeEnemyAttack(
            Transform enemyTransform, 
            EnemyMovement enemyMovement, 
            float recharge, 
            float minimumDistanceToAttackPlayer, 
            LayerMask playerLayerMask, 
            int damage,
            float attackAnimationTime,
            EnemyAnimator animator,
            MonoBehaviour enemyMono)
        {
            _enemyTransform = enemyTransform;
            _enemyMovement = enemyMovement;
            _recharge = recharge;
            _timer = new Timer();
            _playerLayerMask = playerLayerMask;
            _minimumDistanceToAttackPlayer = minimumDistanceToAttackPlayer;
            _damage = damage;
            _animator = animator;
            _attackAnimationTime = attackAnimationTime;
            _enemyMono = enemyMono;
        }

        public void Update()
        {
            _timer.TryTick();
            TryAttack();
        }
        
        private void TryAttack()
        {
            if (!IsWeaponReloaded)
            {
                return;
            }
            
            if (_attack != null || !CanAttackPlayer)
            {
                return;
            }

            _attack = AttackAnimation();
            _enemyMono.StartCoroutine(_attack);
        }

        private IEnumerator AttackAnimation()
        {
            // Ожидаем завершения анимации
            _animator.StartMeleeAttack();
            _enemyMovement.StopAgent();
            
            yield return new WaitForSeconds(_attackAnimationTime);
            _animator.EndMeleeAttack();
            _enemyMovement.ResumeAgent();
            
            // Пытаемся удалить
            if (Physics.SphereCast(_enemyTransform.position, .5f, _enemyTransform.forward, out var hit, _minimumDistanceToAttackPlayer, _playerLayerMask))
            {
                if (hit.collider.TryGetComponent(out IEntityHealth player))
                {
                    IsWeaponReloaded = false;
                    _timer.Start(_recharge, () =>
                    {
                        IsWeaponReloaded = true;
                    });

                    player.TakeDamage(_damage);
                    if (player.IsDied)
                    {
                        _enemyMovement.ResetPlayerPart();
                    }
                    OnTakeDamage?.Invoke();
                }
            }

            _attack = null;
        }

        public void Dispose()
        {
            if (_attack != null)
            {
                _enemyMono.StopCoroutine(_attack);
                _attack = null;
            }
        }
    }
}