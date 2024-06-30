#nullable enable
using Data;
using Enemies;
using Snake;
using UnityEngine;

namespace Factories
{
    public class EnemyFactory
    {
        private readonly Transform _enemiesParent;
        private readonly MeleeEnemyController _meleeEnemyPrefab;
        private readonly SnakeController _player;

        public EnemyFactory(Transform enemiesParent, MeleeEnemyController meleeEnemyPrefab, SnakeController player)
        {
            _enemiesParent = enemiesParent;
            _meleeEnemyPrefab = meleeEnemyPrefab;
            _player = player;
        }
        
        public IEnemyController CreateMeleeEnemy(EnemyData data, Vector3 position)
        {
            var view = Object.Instantiate(_meleeEnemyPrefab, _enemiesParent, false);
            view.Init(data, _player);
            return view;
        }
    }
}