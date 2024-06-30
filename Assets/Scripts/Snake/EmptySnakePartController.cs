#nullable enable
using System;
using Enemies;
using UnityEngine;
using Weapons;

namespace Snake
{
    public class EmptySnakePartController : ISnakePartController
    {
        public event Action? OnDied;
        
        public static ISnakePartController Instance => _instance ??= new EmptySnakePartController();

        private static EmptySnakePartController? _instance;
        
        public Vector3 Position => Vector3.zero;
     
        public bool IsDied => false;

        public Transform Transform => null!;

        public void Move(Vector3 position)
        {
            // nothing
        }

        public void ChooseWeapon(IWeaponController controller)
        {
            // nothing
        }

        public void ChooseEnemy(IEnemyController enemy)
        {
            // nothing
        }

        public void TryShoot()
        {
            // nothing
        }

        public void ToDieWithoutNotification()
        {
            // nothing
        }

        public void TakeDamage(int damage)
        {
            // nothing
        }
    }
}