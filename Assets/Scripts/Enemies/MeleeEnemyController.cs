#nullable enable
namespace Enemies
{
    public class MeleeEnemyController : EnemyControllerBase
    {
        private MeleeEnemyAttack _enemyAttack = null!;
        
        protected override void InitInternal()
        {
            _enemyAttack = new MeleeEnemyAttack(transform, 
                Movement, 
                Data.Recharge, 
                Data.MinimumDistanceToAttackPlayer,
                Data.PlayerLayerMask, 
                Data.Damage,
                Data.AttackAnimationTime,
                EnemyAnimator,
                this);
        }

        private void Update()
        {
            _enemyAttack.Update();
        }

        protected override void OnDestroyInternal()
        {
            base.OnDestroyInternal();
            _enemyAttack.Dispose();
        }
    }
}