#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Data;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ZombieConfig", menuName = "SNT/ZombieConfig", order = 0)]
    public class ZombiesConfig : ScriptableObject
    {
        [Serializable]
        private struct ZombieData
        {
            public string Comment;

            [Header("Максимальное кол-ва здоровья")]
            public int MaxHealth;

            [Header("Урон")] 
            public int Damage;

            [Header("Время перезарядки")] 
            public float Recharge;

            [Header("Мин. расстояние для атаки до игрока")]
            public float MinimumDistanceToAttackPlayer;

            [Header("Маска игрока")] 
            public LayerMask PlayerLayerMask;

            [Header("Время анимации атаки")] 
            public float AttackAnimationTime;
            
            [Header("Скорость движения")] 
            public float Speed;

            [Header("Скорость на поворотах")] 
            public float AngularSpeed;

            [Header("Расстояние для остановки")] 
            public float StoppingDistance;

            [Header("Ускорение")] 
            public float Acceleration;

            [Header("Рекоммендованное растояние до игрока чтобы не менять цель в качестве атаки")]
            public float RecommendedDistanceToPlayer;
        }

        [SerializeField] 
        private ZombieData[] _zombies = null!;

        public ReadOnlyCollection<EnemyData> GetEnemiesData()
        {
            var result = new List<EnemyData>();
            foreach (var zombie in _zombies)
            {
                var data = new EnemyData(
                    zombie.MaxHealth, 
                    zombie.Damage,
                    zombie.Recharge,
                    zombie.MinimumDistanceToAttackPlayer,
                    zombie.PlayerLayerMask,
                    zombie.AttackAnimationTime,
                    
                    zombie.Speed, 
                    zombie.AngularSpeed,
                    zombie.StoppingDistance, 
                    zombie.Acceleration,
                    zombie.RecommendedDistanceToPlayer);
                result.Add(data);
            }

            return result.AsReadOnly();
        }
    }
}