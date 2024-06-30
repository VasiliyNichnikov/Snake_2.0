#nullable enable
using System.Collections.Generic;
using System.Linq;
using Factories;
using Projectiles;
using UnityEngine;

namespace Pools
{
    public class ProjectilePool
    {
        private readonly ProjectileFactory _factory;
        
        private readonly List<IProjectile> _usedSides = new();
        private readonly List<IProjectile> _unusedSides = new();
        
        public ProjectilePool(ProjectileFactory factory)
        {
            _factory = factory;
        }

        public void DestroyUnusedProjectiles(int destroyAmount)
        {
            if (destroyAmount == 0)
            {
                return;
            }

            if (destroyAmount > _unusedSides.Count)
            {
                Debug.LogError("ProjectilePool.DestroyUnusedProjectiles.Can't destroy more than there is in a pool!");
                destroyAmount = _unusedSides.Count;
            }

            for (int i = 0; i < destroyAmount; i++)
            {
                IProjectile projectile = _unusedSides[0];
                projectile.Destroy();
                _unusedSides.RemoveAt(0);
            }
        }

        public void HideAllUsedProjectiles()
        {
            while (_usedSides.Count != 0)
            {
                var projectile = _usedSides[0];
                HideObject(projectile);
            }
        }
        
        public IProjectile GetOrCreateObject(ProjectileType type, bool show = false)
        {
            var selectedProjectile = _unusedSides.FirstOrDefault(projectile => projectile.Type == type);
            if (selectedProjectile != null)
            {
                _unusedSides.Remove(selectedProjectile);
                _usedSides.Add(selectedProjectile);
                if (show)
                {
                    selectedProjectile.Show();
                }

                return selectedProjectile;
            }
            
            var createdProjectile = _factory.Create(type);
            _usedSides.Add(createdProjectile);
            return createdProjectile;
        }
        
        public void HideObject(IProjectile obj)
        {
            if (_usedSides.Contains(obj))
            {
                _usedSides.Remove(obj);
                obj.Hide();
                _unusedSides.Add(obj);
            }
        }
    }
}