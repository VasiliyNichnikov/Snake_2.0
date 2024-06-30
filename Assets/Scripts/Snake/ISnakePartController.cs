using UnityEngine;
using Weapons;

namespace Snake
{
    public interface ISnakePartController
    {
        Vector3 Position { get; }
        
        void Move(Vector3 position);

        void ChooseWeapon(IWeaponController controller);
    }
}