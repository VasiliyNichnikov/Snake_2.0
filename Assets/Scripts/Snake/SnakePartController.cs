#nullable enable
using Data;
using Enemies;
using UnityEngine;
using UnityEngine.AI;

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
        
        private SnakePartData _data;
        private SnakeAnimatorController _snakeAnimatorController = null!;
        private IChoosingTarget _choosingTarget = null!;

        private IEnemyController? _selectedEnemy;
        
        public void Init(SnakePartData data, IChoosingTarget choosingTarget)
        {
            _data = data;

            _choosingTarget = choosingTarget;
            transform.position = data.StartingPosition;
            _snakeAnimatorController = new SnakeAnimatorController(_animator);

            _choosingTarget.OnSelectedEnemy += OnSelectedEnemy;
            
            InitNavMeshAgent();
        }

        public void Move(Vector3 position)
        {
            var distance = Vector3.Distance(position, transform.position);
            if (distance <= _agent.stoppingDistance + 0.1f)
            {
                _snakeAnimatorController.Idle();
            }
            else
            {
                _agent.SetDestination(position);
                _snakeAnimatorController.Walk(_agent.speed);
            }

            if (_selectedEnemy != null)
            {
                transform.LookAt(_selectedEnemy.Target, Vector3.up);
            }
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

        private void OnDestroy()
        {
            _choosingTarget.OnSelectedEnemy -= OnSelectedEnemy;
        }
    }
}