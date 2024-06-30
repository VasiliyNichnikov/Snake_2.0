#nullable enable
using Data;
using Factories;
using Pools;
using UnityEngine;

namespace Projectiles
{
    public class ProjectilesManager
    {
        private readonly ProjectilePool _pool;
        
        public ProjectilesManager(ProjectileData data, Transform parent)
        {
            var factory = new ProjectileFactory(data, parent, this);
            _pool = new ProjectilePool(factory);
        }
        
        public IProjectile GetOrCreateProjectile(ProjectileType type)
        {
            return _pool.GetOrCreateObject(type, true);
        }

        public void RemoveProjectile(IProjectile projectile)
        {
            _pool.HideObject(projectile);
        }
    }
}