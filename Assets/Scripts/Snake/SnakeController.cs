#nullable enable
using System.Collections.Generic;
using Configs;
using Data;
using Factories;
using Map;
using UnityEngine;

namespace Snake
{
    public class SnakeController : MonoBehaviour
    {
        [SerializeField] 
        private SnakePartConfig _snakePartConfig = null!;
        
        private Camera _camera = null!;
        private SnakePartFactory _snakePartFactory = null!;
        
        private readonly List<ISnakePartController> _parts = new List<ISnakePartController>();

        public void Init(SnakeData data)
        {
            _snakePartFactory = data.SnakePartFactory;
            _camera = data.Camera;
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

            var part = _snakePartFactory.Create(data);
            _parts.Add(part);
        }
    }
}