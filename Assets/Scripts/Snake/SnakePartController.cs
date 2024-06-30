#nullable enable
using Data;
using Enemies;
using UnityEngine;
using UnityEngine.AI;
using Weapons;

namespace Snake
{
    /// <summary>
    /// Кусок змеи
    /// </summary>
    public class SnakePartController : MonoBehaviour, ISnakePartController
    {
        public Vector3 Position => transform.position;

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
        }

        public void Move(Vector3 position)
        {
            _agent.SetDestination(position);
        }

        private void OnSelectedEnemy(IEnemyController enemy)
        {
            if (_selectedEnemy != null && !Equals(_selectedEnemy, enemy))
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
            controller.Apply(_storage);
        }

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
    }
}