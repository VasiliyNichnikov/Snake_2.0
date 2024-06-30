#nullable enable
using System;
using System.Linq;
using Entities;
using Snake;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyMovement : IDisposable
    {
        public Vector3 Position => _enemyTransform.position;
        
        public Transform EnemyTransform => _enemyTransform;
        
        public Vector3 PointForMovement => _pointForMovement ?? Vector3.zero;

        private readonly NavMeshAgent _agent;
        private readonly Transform _enemyTransform;
        private readonly Vector3 _startingPosition;
        private readonly SnakeController _player;
        private readonly float _recommendedDistanceToPlayer;
        private readonly IEntityHealth _health;
        private readonly EnemyAnimator _animator;
        
        private Vector3? _pointForMovement;

        private Transform? _selectedPlayerPart;
        
        private Transform? SelectedPlayerPart
        {
            get
            {
                if (_selectedPlayerPart != null)
                {
                    var distance = Vector3.Distance(_enemyTransform.position, _selectedPlayerPart.position);
                    if (distance <= _recommendedDistanceToPlayer)
                    {
                        return _selectedPlayerPart;
                    }
                }
                
                if (_player.Parts.Count != 0)
                {
                    var minDistance = Mathf.Infinity;
                    Transform? selectedPlayerPart = null;
                    foreach (var part in _player.Parts.Where(p => !p.IsDied))
                    {
                        var distance = Vector3.Distance(part.Position, _enemyTransform.position);
                        
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            selectedPlayerPart = part.Transform;
                        }
                    }

                    if (selectedPlayerPart == null)
                    {
                        Debug.LogError("EnemyMovement.SelectedPlayerPart: not found player part.");
                        return null;
                    }

                    _selectedPlayerPart = selectedPlayerPart;
                    return _selectedPlayerPart;
                }

                Debug.LogWarning("EnemyMovement.SelectedPlayerPart: The player's part could not be found.");
                return null;
            }
        }


        public EnemyMovement(
            NavMeshAgent agent, 
            Transform enemyTransform, 
            SnakeController player, 
            Vector3 startingPosition,
            float recommendedDistanceToPlayer, 
            IEntityHealth health,
            EnemyAnimator animator)
        {
            _agent = agent;
            _enemyTransform = enemyTransform;
            _startingPosition = startingPosition;
            _player = player;
            _recommendedDistanceToPlayer = recommendedDistanceToPlayer;
            _health = health;
            _animator = animator;
            
            
            _health.OnDied += OnDied;
        }

        public float GetDistanceToPlayer()
        {
            var selectedPlayerPart = SelectedPlayerPart;
            if (selectedPlayerPart == null)
            {
                return Mathf.Infinity;
            }
            
            return Vector3.Distance(_enemyTransform.position, selectedPlayerPart.position);
        }
        
        public void ResetPlayerPart()
        {
            _selectedPlayerPart = null;
        }
        
        public void StopAgent()
        {
            _agent.isStopped = true;
        }

        public void ResumeAgent()
        {
            _agent.isStopped = false;
        }

        public void MoveToPlayer()
        {
            if (_health.IsDied)
            {
                return;
            }

            var selectedPart = SelectedPlayerPart;
            if (selectedPart == null)
            {
                return;
            }
            
            var distance = Vector3.Distance(selectedPart.position, _enemyTransform.position);
            if (distance <= _agent.stoppingDistance || _agent.isStopped)
            {
                _animator.Idle();
                return;
            }
            
            _animator.Walk(_agent.speed);
            _pointForMovement = selectedPart.position;
            _agent.SetDestination(_pointForMovement.Value);
        }
        
        public void RotateToPlayer()
        {
            var selectedPlayerPart = SelectedPlayerPart;
            if (selectedPlayerPart == null)
            {
                return;
            }
            
            var direction = selectedPlayerPart.position - _enemyTransform.position;
            var rotation = Vector3.RotateTowards(_enemyTransform.forward, direction, 360, 1f);
            _enemyTransform.rotation = Quaternion.LookRotation(rotation);
        }

        public void ReturnToStartingPoint()
        {
            _enemyTransform.position = _startingPosition;
        }

        public float GetDistanceToSelectedPoint(Vector3 point)
        {
            var path = new NavMeshPath();
            _agent.CalculatePath(point, path);
            return GetPathDistance(path);
        }

        private static float GetPathDistance(NavMeshPath path)
        {
            var corners = path.corners;
            var distance = .0f;
            for (var i = 0; i < corners.Length - 1; i++)
            {
                distance += Vector3.Distance(corners[i], corners[i + 1]);
            }

            return distance;
        }

        public void OnAgent()
        {
            _agent.enabled = true;
        }

        public void OffAgent()
        {
            _agent.enabled = false;
        }
        
        public bool IsPathCompleted()
        {
            // Порог завершения пути
            const float pathEndThreshold = 0.2f;
            return _agent.remainingDistance <= _agent.stoppingDistance + pathEndThreshold;
        }

        private void OnDied()
        {
            StopAgent();
        }
        
        public void Dispose()
        {
            _health.OnDied -= OnDied;
        }
    }
}