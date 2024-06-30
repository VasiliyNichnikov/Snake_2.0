#nullable enable
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Levels
{
    public class TestLevel : MonoBehaviour, ILevel
    {
        public IReadOnlyCollection<IEnemyController> Enemies => _enemies;

        [SerializeField] 
        private EnemyControllerBase[] _enemies = null!;
    }
}