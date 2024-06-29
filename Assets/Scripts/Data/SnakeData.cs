#nullable enable
using Factories;
using UnityEngine;

namespace Data
{
    public readonly struct SnakeData
    {
        public readonly Camera Camera;
        public readonly SnakePartFactory SnakePartFactory;

        public SnakeData(Camera camera, SnakePartFactory snakePartFactory)
        {
            Camera = camera;
            SnakePartFactory = snakePartFactory;
        }
    }
}