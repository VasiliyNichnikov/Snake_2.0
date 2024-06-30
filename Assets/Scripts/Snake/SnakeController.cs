#nullable enable
using System;
using System.Collections.Generic;
using Configs;
using Data;
using Enemies;
using Factories;
using Levels;
using UnityEngine;
using Weapons;

namespace Snake
{
    public class SnakeController : MonoBehaviour, IChoosingEnemyTarget, IChoosingWeapon, IChoosingAlly
    {
        public IReadOnlyCollection<ISnakePartController> Parts => _parts;
        
        public event Action<IEnemyController>? OnSelectedEnemy;
        public Vector3 StartedPosition => _startedPosition.position;

        public bool IsDied => _parts.Count == 0;

        private readonly List<ISnakePartController> _parts = new List<ISnakePartController>();

        [SerializeField] 
        private SnakePartConfig _snakePartConfig = null!;
        
        [SerializeField]
        private int _maximumAttackDistance;

        [SerializeField] private LayerMask _groundMask;

        [SerializeField] 
        private Transform _startedPosition = null!;

        private Camera _camera = null!;
        private SnakePartFactory _snakePartFactory = null!;
        private ILevel _level = null!;
        private WeaponsManager _weapons = null!;
        private Vector3 _previewPointPosition;

        private IEnemyController? _selectedEnemy;

        public void Init(SnakeData data, ILevel level)
        {
            _snakePartFactory = data.SnakePartFactory;
            _camera = data.Camera;
            _level = level;
            _weapons = new WeaponsManager(data.WeaponData, data.ProjectilesManager, DistributeWeaponsToEveryone);
            _previewPointPosition = Vector3.zero;

            // Стартовая часть
            AddPart();
        }

        public void UpdateSnake()
        {
#if UNITY_EDITOR || DEBUG
            if (Input.GetKeyDown(KeyCode.R))
            {
                AddPart();
            }
#endif
            
            if (_parts.Count == 0)
            {
                return;
            }

            FoundEnemy();
            Move();
            TryShoot();
        }

        private void Move()
        {
            if (_parts.Count > 1)
            {
                for (var i = 1; i < _parts.Count; i++)
                {
                    var previewSnakePart = _parts[i - 1];
                    _parts[i].Move(previewSnakePart.Position);
                }
            }
            
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _groundMask))
            {
                var distance = Vector3.Distance(_previewPointPosition, hit.point);
                
                if (distance <= 0.1f)
                {
                    return;
                }
                _previewPointPosition = hit.point;
                _parts[0].Move(hit.point);
            }
        }

        public void ChooseWeapon(WeaponType type) => _weapons.ChooseWeapon(type);
        
        private void FoundEnemy()
        {
            if (_parts.Count == 0)
            {
                return;
            }

            var enemies = _level.LivingEnemies;
            if (enemies.Count == 0)
            {
                return;
            }
            
            var firstPart = _parts[0];

            IEnemyController? selectedEnemy = null;
            var minDistance = Mathf.Infinity;
            foreach (var enemy in enemies)
            {
                var distance = Vector3.Distance(enemy.Target.position, firstPart.Position);

                if (distance > _maximumAttackDistance)
                {
                    continue;
                }
                
                if (distance < minDistance)
                {
                    minDistance = distance;
                    selectedEnemy = enemy;
                }
            }

            if (selectedEnemy != null)
            {
                if (_selectedEnemy != null && Equals(_selectedEnemy, selectedEnemy))
                {
                    return;
                }

                _selectedEnemy = selectedEnemy;
                OnSelectedEnemy?.Invoke(selectedEnemy);
            }
        }

        public void AddPart()
        {
            var startingPosition = _parts.Count != 0 ? _parts[^1].Position : _startedPosition.position;
            var stoppingDistance = _parts.Count != 0 ? _snakePartConfig.StoppingDistance : 0.0f;
            
            var data = new SnakePartData(
                startingPosition, 
                _snakePartConfig.Speed, 
                _snakePartConfig.AngularSpeed,
                stoppingDistance,
                _snakePartConfig.Acceleration);

            var part = _snakePartFactory.Create(data, this, this, this);
            part.OnDied += OnDiedPart;
            _parts.Add(part);

            if (_selectedEnemy != null)
            {
                part.ChooseEnemy(_selectedEnemy);
            }

            if (_weapons.SelectedWeapon != null)
            {
                part.ChooseWeapon(_weapons.SelectedWeapon.Clone());
            }
        }

        private void TryShoot()
        {
            const int leftMouseButtonDown = 0;
            if (Input.GetMouseButtonDown(leftMouseButtonDown) || Input.GetMouseButton(leftMouseButtonDown))
            {
                foreach (var part in _parts)
                {
                    part.TryShoot();
                }
            }
        }

        private void DistributeWeaponsToEveryone(IWeaponController weaponController)
        {
            foreach (var part in _parts)
            {
                part.ChooseWeapon(weaponController.Clone());
            }
        }

        private void OnDiedPart()
        {
            var startDie = false;
            for (var i = 0; i < _parts.Count; i++)
            {
                if (startDie)
                {
                    _parts[i].ToDieWithoutNotification();
                    _parts.RemoveAt(i);
                    i--;
                    continue;
                }
                
                var part = _parts[i];
                if (part.IsDied)
                {
                    _parts.RemoveAt(i);
                    startDie = true;
                    i--;
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var part in _parts)
            {
                part.OnDied -= OnDiedPart;
            }
        }
    }
}