#nullable enable
using System;
using Data;
using Enemies;
using Entities;
using UnityEngine;
using UnityEngine.AI;
using Weapons;

namespace Snake
{
    /// <summary>
    /// Кусок змеи
    /// </summary>
    public class SnakePartController : MonoBehaviour, ISnakePartController, IEntityHealth
    {
        public bool IsDied { get; private set; }
        
        public Vector3 Position => transform.position;
        public Transform Transform => transform;
        
        public event Action? OnDied;

        [SerializeField] 
        private NavMeshAgent _agent = null!;

        [SerializeField] 
        private Animator _animator = null!;

        [SerializeField] 
        private WeaponStorage _storage = null!;
        
        private SnakePartData _data;
        private SnakeAnimatorController _snakeAnimatorController = null!;
        private IChoosingEnemyTarget _choosingEnemyTarget = null!;
        private IChoosingWeapon _choosingWeapon = null!;

        private IEnemyController? _selectedEnemy;
        private IWeaponController? _weaponController;
        
        public void Init(SnakePartData data, IChoosingEnemyTarget choosingEnemyTarget, IChoosingWeapon choosingWeapon)
        {
            _data = data;

            _choosingEnemyTarget = choosingEnemyTarget;
            transform.position = data.StartingPosition;
            _snakeAnimatorController = new SnakeAnimatorController(_animator);
            _choosingWeapon = choosingWeapon;

            _choosingEnemyTarget.OnSelectedEnemy += OnSelectedEnemy;
            
            InitNavMeshAgent();
            _storage.Init(_snakeAnimatorController);
        }

        private void Update()
        {
            if (IsDied)
            {
                return;
            }
            
            var distance = Vector3.Distance(_agent.pathEndPosition, transform.position);
            if (distance <= _agent.stoppingDistance)
            {
                _snakeAnimatorController.Idle();
            }
            else
            {
                _snakeAnimatorController.Walk(_agent.speed);
            }
            
            if (_selectedEnemy != null)
            {
                transform.LookAt(_selectedEnemy.Target, Vector3.up);
            }

            _weaponController?.Update();
        }
        
        /// <summary>
        /// Одна жизнь
        /// </summary>
        public void TakeDamage(int damage) => Die(true);

        public void Move(Vector3 position)
        {
            if (IsDied)
            {
                return;
            }
            
            _agent.SetDestination(position);
        }

        private void OnSelectedEnemy(IEnemyController enemy)
        {
            if (_selectedEnemy != null && Equals(_selectedEnemy, enemy))
            {
                return;
            }

            _selectedEnemy = enemy;
        }

        private void InitNavMeshAgent()
        {
            _agent.speed = _data.Speed;
            _agent.angularSpeed = _data.AngularSpeed;
            _agent.stoppingDistance = _data.StoppingDistance;
            _agent.acceleration = _data.Acceleration;
        }

        public void ChooseWeapon(IWeaponController controller)
        {
            if (IsDied)
            {
                return;
            }
            
            _weaponController = controller;
            controller.Apply(_storage, _snakeAnimatorController);
        }

        public void ChooseEnemy(IEnemyController enemy)
        {
            _selectedEnemy = enemy;
        }

        public void TryShoot()
        {
            if (IsDied)
            {
                return;
            }
            
            _weaponController?.Shoot();
        }

        public void ToDieWithoutNotification() => Die(false);

        private void OnTriggerEnter(Collider other)
        {
            TryCollectWeapon(other.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            TryCollectWeapon(other.gameObject);
        }

        private void TryCollectWeapon(GameObject? gameObj)
        {
            if (gameObj == null)
            {
                return;
            }

            var view = gameObj.GetComponent<WeaponView>();
            if (view != null && !view.InHand)
            {
                _choosingWeapon.ChooseWeapon(view.Type);
                view.PickUp();
            }
        }

        private void OnDestroy()
        {
            _choosingEnemyTarget.OnSelectedEnemy -= OnSelectedEnemy;
        }

        private void Die(bool needNotification)
        {
            if (IsDied)
            {
                return;
            }
            
            IsDied = true;
            _snakeAnimatorController.Die();

            if (needNotification)
            {
                OnDied?.Invoke();
            }
        }
    }
}