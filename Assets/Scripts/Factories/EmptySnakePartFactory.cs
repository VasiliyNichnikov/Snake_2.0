#nullable enable
using Snake;
using UnityEngine;

namespace Factories
{
    public class EmptySnakePartFactory
    {
        public EmptySnakePart Create(Vector3 position)
        {
            return null!;
        }
    }
}