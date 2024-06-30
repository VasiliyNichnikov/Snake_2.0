using Enemies;
using Entities;
using UnityEngine;
using Weapons;

namespace Snake
{
    public interface ISnakePartController : IEntityHealth
    {
        Vector3 Position { get; }
        
        Transform Transform { get; }
        
        void Move(Vector3 position);

        void ChooseWeapon(IWeaponController controller);

        void ChooseEnemy(IEnemyController enemy);

        void TryShoot();

        void ToDieWithoutNotification();
    }
}