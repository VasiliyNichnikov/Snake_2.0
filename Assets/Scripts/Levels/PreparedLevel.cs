#nullable enable
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Data;
using Enemies;
using Snake;
using UnityEngine;

namespace Levels
{
    public class PreparedLevel : MonoBehaviour, ILevel
    {
        public IReadOnlyCollection<IEnemyController> LivingEnemies => _enemies.Where(e => !e.IsDied).ToList();
        
        [SerializeField] 
        private EnemyControllerBase[] _enemies = null!;
        
        private SnakeController _player = null!;
        
        public void Init(ReadOnlyCollection<EnemyData> collection, SnakeController player)
        {
            _player = player;
            foreach (var enemy in _enemies)
            {
                enemy.Init(collection[0], player);
            }
            
        }
        
        private void Update()
        {
            MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            if (_player.Parts.Count == 0)
            {
                return;
            }
            
            foreach (var enemy in LivingEnemies)
            {
                enemy.Movement.MoveToPlayer();
            }
        }
    }
}