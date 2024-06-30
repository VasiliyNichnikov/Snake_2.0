#nullable enable
using System;
using System.Collections.Generic;
using Configs;
using Data;
using Enemies;
using Factories;
using Levels;
using Map;
using UnityEngine;
using Weapons;

namespace Snake
{
    public class SnakeController : MonoBehaviour, IChoosingEnemyTarget, IChoosingWeapon
    {
        public event Action<IEnemyController>? OnSelectedEnemy;
        
        [SerializeField] 
        private SnakePartConfig _snakePartConfig = null!;
        
        private Camera _camera = null!;
        private SnakePartFactory _snakePartFactory = null!;
        private ILevel _level = null!;
        private WeaponsManager _weapons = null!;

        private Vector3 _previewPointPosition;

        private readonly List<ISnakePartController> _parts = new List<ISnakePartController>();

        public void Init(SnakeData data, ILevel level)
        {
            _snakePartFactory = data.SnakePartFactory;
            _camera = data.Camera;
            _level = level;
            _weapons = new WeaponsManager(data.WeaponData, data.ProjectilesManager, DistributeWeaponsToEveryone);
            _previewPointPosition = Vector3.zero;
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
            if (Physics.Raycast(ray, out var hit))
            {
                if (!hit.collider.GetComponent<Ground>())
                {
                    return;
                }

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
            // TODO: дописать: нужно выбрать врага
            foreach (var enemy in _level.Enemies)
            {
                OnSelectedEnemy?.Invoke(enemy);
            }
        }

        private void AddPart()
        {
            var startingPosition = _parts.Count != 0 ? _parts[^1].Position : Vector3.zero;
            var stoppingDistance = _parts.Count != 0 ? _snakePartConfig.StoppingDistance : 0.0f;
            
            var data = new SnakePartData(
                startingPosition, 
                _snakePartConfig.Speed, 
                _snakePartConfig.AngularSpeed,
                stoppingDistance,
                _snakePartConfig.Acceleration);

            var part = _snakePartFactory.Create(data, this, this);
            _parts.Add(part);

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
    }
}