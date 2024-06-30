#nullable enable
using UnityEngine;

namespace Enemies
{
    public class EnemyAnimator
    {
        private const int EmptyHands = 0;
        private const int Knife = 12;
        
        private readonly Animator _animator;

        private static readonly int Death = Animator.StringToHash("Death_b"); 
        
        private static readonly int StaticB = Animator.StringToHash("Static_b");
        private static readonly int StaticF = Animator.StringToHash("Speed_f");
        private static readonly int WeaponType = Animator.StringToHash("WeaponType_int");
        
        public EnemyAnimator(Animator animator)
        {
            _animator = animator;
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

        public void StartMeleeAttack()
        {
            _animator.SetInteger(WeaponType, Knife);
        }

        public void EndMeleeAttack()
        {
            _animator.SetInteger(WeaponType, EmptyHands);
        }
        
        public void Die()
        {
            _animator.SetBool(Death, true);
        }
    }
}