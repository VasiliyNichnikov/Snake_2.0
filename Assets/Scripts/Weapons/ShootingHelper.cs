#nullable enable
using Enemies;
using Entities;
using Projectiles;
using Snake;

namespace Weapons
{
    public static class ShootingHelper
    {
        public static void ShootFromPlayer(ProjectileType type, IWeaponViewInHand weaponViewInHand, ProjectilesManager projectilesManager)
        {
            var startingPosition = weaponViewInHand.GetRandomPoint();
            var controller = new DefaultProjectileController(startingPosition.position, startingPosition.forward);
            var projectile = projectilesManager.GetOrCreateProjectile(type);
            projectile.Init(controller);
            projectile.ToRun(collider =>
            {
                if (collider == null || collider.gameObject == null)
                {
                    return false;
                }

                if (collider.gameObject.TryGetComponent<IEnemyController>(out var component))
                {
                    ((IEntityHealth)component).TakeDamage(projectile.Damage);
                    return true;
                }

                return collider.transform.GetComponent<ISnakePartController>() == null;
            });
        }
    }
}