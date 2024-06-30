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

namespace Snake
{
    public class SnakeController : MonoBehaviour, IChoosingTarget
    {
        public event Action<IEnemyController>? OnSelectedEnemy;
        
        [SerializeField] 
        private SnakePartConfig _snakePartConfig = null!;
        
        private Camera _camera = null!;
        private SnakePartFactory _snakePartFactory = null!;
        private ILevel _level = null!;
        

        private readonly List<ISnakePartController> _parts = new List<ISnakePartController>();

        public void Init(SnakeData data, ILevel level)
        {
            _snakePartFactory = data.SnakePartFactory;
            _camera = data.Camera;
            _level = level;
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
        }

        private void Move()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (!hit.collider.GetComponent<Ground>())
                {
                    return;
                }

                for (var i = 0; i < _parts.Count; i++)
                {
                    var snakePart = _parts[i];
                    if (i == 0)
                    {
                        snakePart.Move(hit.point);
                        continue;
                    }

                    var previewSnakePart = _parts[i - 1];
                    snakePart.Move(previewSnakePart.Position);
                }
            }
        }

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

            var part = _snakePartFactory.Create(data, this);
            _parts.Add(part);
        }
    }
}