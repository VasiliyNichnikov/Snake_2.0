#nullable enable
using Factories;
using Projectiles;
using UnityEngine;

namespace Data
{
    public readonly struct SnakeData
    {
        public readonly Camera Camera;
        public readonly SnakePartFactory SnakePartFactory;
        public readonly WeaponData WeaponData;
        public readonly ProjectilesManager ProjectilesManager;

        public SnakeData(
            Camera camera, 
            SnakePartFactory snakePartFactory, 
            WeaponData weaponData, 
            ProjectilesManager projectilesManager)
        {
            Camera = camera;
            SnakePartFactory = snakePartFactory;
            WeaponData = weaponData;
            ProjectilesManager = projectilesManager;
        }
    }
}