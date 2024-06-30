#nullable enable
using Factories;
using UnityEngine;

namespace Data
{
    public readonly struct SnakeData
    {
        public readonly Camera Camera;
        public readonly SnakePartFactory SnakePartFactory;
        public readonly WeaponData WeaponData;

        public SnakeData(Camera camera, SnakePartFactory snakePartFactory, WeaponData weaponData)
        {
            Camera = camera;
            SnakePartFactory = snakePartFactory;
            WeaponData = weaponData;
        }
    }
}