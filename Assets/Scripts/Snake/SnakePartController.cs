#nullable enable
using Data;
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
        
        public void Init(SnakePartData data)
        {
            _data = data;

            transform.position = data.StartingPosition;
            _snakeAnimatorController = new SnakeAnimatorController(_animator);
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
        }

        private void InitNavMeshAgent()
        {
            _agent.speed = _data.Speed;
            _agent.angularSpeed = _data.AngularSpeed;
            _agent.stoppingDistance = _data.StoppingDistance;
            _agent.acceleration = _data.Acceleration;
        }
    }
}