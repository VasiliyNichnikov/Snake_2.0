#nullable enable
using System.Linq;
using Snake;
using UnityEngine;

namespace Scene
{
    public class MovementCamera : MonoBehaviour
    {
        [SerializeField] private SnakeController _snake = null!;

        [SerializeField] private float _smooth;
        
        private Transform? _firstPart;
        private Vector3 _offset;

        private void Start()
        {
            _offset = transform.position - _snake.StartedPosition;
        }

        private void LateUpdate()
        {
            if (_firstPart == null)
            {
                if (_snake.Parts.Count != 0)
                {
                    _firstPart = _snake.Parts.First().Transform;
                }
                return;
            }
            
            transform.position = Vector3.Lerp (transform.position, _firstPart.position + _offset, Time.deltaTime * _smooth);
        }
    }
}