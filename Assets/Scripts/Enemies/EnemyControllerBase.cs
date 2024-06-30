#nullable enable
using System;
using Data;
using Entities;
using Snake;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public abstract class EnemyControllerBase : MonoBehaviour, IEnemyController, IEntityHealth
    {
        public event Action? OnDied;
        
        public bool IsDied { get; private set; }
        
        [SerializeField] 
        private Animator _animator = null!;

        [SerializeField] 
        private SkinsStorage _skinsStorage = null!;

        [SerializeField] 
        private NavMeshAgent _agent = null!;

        protected EnemyAnimator EnemyAnimator = null!;

        public Transform Target => transform;
        public EnemyMovement Movement { get; private set; } = null!;

        protected EnemyData Data;
        private int _currentHealth;


        public void Init(EnemyData data, SnakeController player)
        {
            Data = data;
            EnemyAnimator = new EnemyAnimator(_animator);
            Movement = new EnemyMovement(
                _agent, 
                transform, 
                player, 
                transform.position, 
                data.RecommendedDistanceToPlayer,
                this,
                EnemyAnimator);

            _currentHealth = Data.MaxHealth;
            _skinsStorage.SetRandomSkin();

            InitNavMeshAgent();
            InitInternal();
        }

        public void TakeDamage(int damage)
        {
            if (IsDied)
            {
                return;
            }

            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                EnemyAnimator.Die();
                IsDied = true;
                OnDied?.Invoke();
            }
        }

        protected abstract void InitInternal();

        private void InitNavMeshAgent()
        {
            _agent.speed = Data.Speed;
            _agent.angularSpeed = Data.AngularSpeed;
            _agent.stoppingDistance = Data.StoppingDistance;
            _agent.acceleration = Data.Acceleration;
        }

        private void OnDestroy()
        {
            OnDestroyInternal();
        }

        protected virtual void OnDestroyInternal()
        {
            Movement.Dispose();
        }
    }
}