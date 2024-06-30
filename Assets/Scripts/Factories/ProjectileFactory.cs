#nullable enable
using System;
using System.Collections.Generic;
using Data;
using Projectiles;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Factories
{
    public class ProjectileFactory
    {
        private readonly ProjectileData _data;
        private readonly Transform _parent;
        private readonly ProjectilesManager _projectilesManager;
        
        public ProjectileFactory(ProjectileData data, Transform parent, ProjectilesManager projectilesManager)
        {
            _data = data;
            _parent = parent;
            _projectilesManager = projectilesManager;
        }
        
        public IProjectile Create(ProjectileType type)
        {
            switch (type)
            {
                case ProjectileType.DesertEagle:
                case ProjectileType.Kalash:
                case ProjectileType.Minigun:
                    var data = GetRandomData(_data.DefaultProjectiles);
                    var view = Object.Instantiate(data.Prefab, _parent, false);
                    view.InitFactory(data, _projectilesManager);
                    return view;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static T GetRandomData<T>(IReadOnlyList<T> collection) where T: struct
        {
            var random = Random.Range(0, collection.Count - 1);
            return collection[random];
        }
    }
}