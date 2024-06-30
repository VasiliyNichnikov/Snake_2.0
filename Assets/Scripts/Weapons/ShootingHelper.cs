#nullable enable
using Projectiles;

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
                // TODO: доделать
                return collider != null;
            });
        }
    }
}