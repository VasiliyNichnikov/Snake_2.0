#nullable enable
using UnityEngine;

namespace Data
{
    public readonly struct EnemyData
    {
        public readonly int MaxHealth;
        public readonly int Damage;
        public readonly float Recharge;
        public readonly float MinimumDistanceToAttackPlayer;
        public readonly LayerMask PlayerLayerMask;
        public readonly float AttackAnimationTime;
        
        public readonly float Speed;
        public readonly float AngularSpeed;
        public readonly float StoppingDistance;
        public readonly float Acceleration;
        public readonly float RecommendedDistanceToPlayer;

        public EnemyData(int maxHealth, 
            int damage, 
            float recharge,
            float minimumDistanceToAttackPlayer,
            LayerMask playerLayerMask,
            float attackAnimationTime,
            
            float speed,
            float angularSpeed, 
            float stoppingDistance,
            float acceleration,
            float recommendedDistanceToPlayer)
        {
            MaxHealth = maxHealth;
            Damage = damage;
            Recharge = recharge;
            MinimumDistanceToAttackPlayer = minimumDistanceToAttackPlayer;
            PlayerLayerMask = playerLayerMask;
            AttackAnimationTime = attackAnimationTime;
            
            Speed = speed;
            AngularSpeed = angularSpeed;
            StoppingDistance = stoppingDistance;
            Acceleration = acceleration;
            RecommendedDistanceToPlayer = recommendedDistanceToPlayer;
        }
    }
}