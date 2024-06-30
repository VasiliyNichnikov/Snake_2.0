#nullable enable
using UnityEngine;
using Weapons;

namespace Snake
{
    public class SnakeAnimatorController : IWeaponAnimator
    {
        private static readonly int StaticB = Animator.StringToHash("Static_b");
        private static readonly int StaticF = Animator.StringToHash("Speed_f");
        private static readonly int WeaponType = Animator.StringToHash("WeaponType_int");
        private static readonly int SingleShoot = Animator.StringToHash("Shoot_b");
        
        private readonly Animator _animator;

        public SnakeAnimatorController(Animator animator)
        {
            _animator = animator;
        }

        public void PlaySingleShoot()
        {
            _animator.SetBool(SingleShoot, true);
        }

        public void Idle()
        {
            _animator.SetBool(StaticB, true);
            _animator.SetFloat(StaticF, 0.0f);
        }

        public void Walk(float speed)
        {
            _animator.SetBool(StaticB, false);
            _animator.SetFloat(StaticF, speed);
        }

        public void SetWeapon(WeaponType? weapon)
        {
            var weaponId = weapon == null ? 0 : (int)weapon;
            _animator.SetInteger(WeaponType, weaponId);
        }
    }
}