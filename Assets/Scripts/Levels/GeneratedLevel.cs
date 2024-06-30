#nullable enable
using System.Collections.Generic;
using System.Linq;
using Data;
using Enemies;
using Entities;
using Factories;
using Snake;
using UnityEngine;
using Weapons;

namespace Levels
{
    public class GeneratedLevel : ILevel
    {
        private class CurrentWave
        {
            // Сколько врагов осталось
            public int NumberEnemies;
            
            // Сколько союзников осталось
            public int NumberAllies;

            public CurrentWave(int numberEnemies, int numberAllies)
            {
                NumberEnemies = numberEnemies;
                NumberAllies = numberAllies;
            }
        }
        
        public IReadOnlyCollection<IEnemyController> LivingEnemies => _enemies.Where(e => !((IEntityHealth)e).IsDied).ToList();

        private readonly List<WeaponType> WeaponTypes = new List<WeaponType>
        {
            WeaponType.Kalash,
            WeaponType.Minigun,
            WeaponType.DesertEagle
        };

        private readonly List<IEnemyController> _enemies;
        private readonly List<EmptySnakePart> _emptyParts;
        private readonly List<WeaponView> _weapons;

        private readonly MapGeneratorData _data;
        private readonly EnemyFactory _enemyFactory;
        private readonly EmptySnakePartFactory _emptySnakePartFactory;
        private readonly WeaponFactory _weaponFactory;

        private CurrentWave? _savedData;
        private int _wave;
        
        public GeneratedLevel(MapGeneratorData data, 
            EnemyFactory enemyFactory, 
            EmptySnakePartFactory emptySnakePartFactory,
            WeaponFactory weaponFactory)
        {
            _enemies = new List<IEnemyController>();
            _emptyParts = new List<EmptySnakePart>();
            _weapons = new List<WeaponView>();

            _data = data;
            _enemyFactory = enemyFactory;
            _emptySnakePartFactory = emptySnakePartFactory;
            _weaponFactory = weaponFactory;
            _wave = 0;

            GenerateStartingItems();
        }

        private void GenerateStartingItems()
        {
            var wave = GetCurrentWave();
            _savedData = new CurrentWave(wave.NumberEnemies, wave.NumberAllies);
            
            for (var i = 0; i < wave.NumberOfEnemiesAtSameTime; i++)
            {
                _savedData.NumberEnemies--;
                if (_savedData.NumberEnemies < 0)
                {
                    Debug.LogError("GeneratedLevel.GenerateStartingItems: enemies ran out during initialization.");
                    continue;
                }
                
                var data = GetRandomDataFromCollection(wave.EnemiesData);
                var randomPosition = GetRandomDataFromCollection(_data.EnemiesTransforms).position;
                var enemy = _enemyFactory.CreateMeleeEnemy(data, randomPosition);
                _enemies.Add(enemy);
            }

            for (var i = 0; i < wave.NumberOfAlliesAtSameTime; i++)
            {
                _savedData.NumberAllies--;
                if (_savedData.NumberAllies < 0)
                {
                    Debug.LogError("GeneratedLevel.GenerateStartingItems: allies ran out during initialization.");
                    continue;
                }
                
                var randomPosition = GetRandomDataFromCollection(_data.AlliesTransforms).position;
                var emptyPart = _emptySnakePartFactory.Create(randomPosition);
                _emptyParts.Add(emptyPart);
            }

            for (var i = 0; i < wave.NumberOfWeaponsAtSameTime; i++)
            {
                var data = GetRandomDataFromCollection(WeaponTypes);
                var randomPosition = GetRandomDataFromCollection(_data.WeaponsTransforms).position;
                var weapon = _weaponFactory.Create(data, randomPosition);
                _weapons.Add(weapon);
            }
        }

        private WaveData GetCurrentWave()
        {
            if (_data.Waves.Count == 0)
            {
                Debug.LogError("GeneratedLevel.GetCurrentWave: number waves is null.");
                return new WaveData();
            }

            if (_wave < 0)
            {
                Debug.LogError("GeneratedLevel.GetCurrentWave: number of waves is less than 0.");
                return new WaveData();
            }

            if (_wave >= _data.Waves.Count)
            {
                Debug.LogError("GeneratedLevel.GetCurrentWave: number of waves is greater than the list.");
                return new WaveData();
            }

            var currentWave = _data.Waves[_wave];
            return currentWave;
        }

        private T GetRandomDataFromCollection<T>(IReadOnlyList<T> collection)
        {
            var index = Random.Range(0, collection.Count - 1);
            return collection[index];
        }
    }
}