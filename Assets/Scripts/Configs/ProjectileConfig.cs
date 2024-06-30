#nullable enable
using System;
using System.Collections.Generic;
using Data;
using Projectiles;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "SNT/ProjectileConfig", order = 0)]
    public class ProjectileConfig : ScriptableObject
    {
        [Serializable]
        private struct ProjectileData
        {
            public string Comment;

            [Header("Тип оружия")]
            public ProjectileType Type;

            [Header("Скорость полета")] 
            public float Speed;

            [Header("Урон")] 
            public int Damage;

            [Header("Префаб снаряда")] 
            public DefaultProjectile Prefab;
        }

        [SerializeField] 
        private ProjectileData[] _projectiles = null!;

        public Data.ProjectileData GetData()
        {
            var defaultProjectiles = new List<DefaultProjectileData>();

            foreach (var projectile in _projectiles)
            {
                switch (projectile.Type)
                {
                    case ProjectileType.DesertEagle:
                    case ProjectileType.Kalash:
                    case ProjectileType.Minigun:
                        defaultProjectiles.Add(new DefaultProjectileData(
                            projectile.Type,
                            projectile.Speed, 
                            projectile.Damage,
                            projectile.Prefab));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return new Data.ProjectileData(defaultProjectiles.AsReadOnly());
        }
    }
}