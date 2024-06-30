#nullable enable
using Data;
using Projectiles;
using UnityEngine;
using Utils;

namespace Weapons
{
    public class WeaponDesertEagleController : IWeaponController
    {
        private readonly WeaponDesertEagleData _data;
        private readonly ProjectilesManager _projectilesManager;
        private readonly Timer _timer;

        private bool _isWeaponReloaded = true;
        private IWeaponViewInHand? _weaponViewInHand;
        private IWeaponAnimator? _weaponAnimator;

        public WeaponDesertEagleController(WeaponDesertEagleData data, ProjectilesManager projectilesManager)
        {
            _data = data;
            _timer = new Timer();
            _projectilesManager = projectilesManager;
        }

        public void Update()
        {
            _timer.TryTick();
        }

        public void Apply(IWeaponVisitor visitor, IWeaponAnimator animator)
        {
            _weaponViewInHand = visitor.Visit(_data);
            _weaponAnimator = animator;
        }

        public IWeaponController Clone()
        {
            return new WeaponDesertEagleController(_data, _projectilesManager);
        }

        public void Shoot()
        {
            if (_weaponViewInHand == null)
            {
                Debug.LogError("WeaponDesertEagleController.Shoot: weaponViewInHand is null.");
                return;
            }
            
            if (!_isWeaponReloaded)
            {
                return;
            }
            
            _isWeaponReloaded = false;
            _timer.Start(_data.RateOfFire, () =>
            {
                _isWeaponReloaded = true;
            });

            _weaponAnimator?.PlaySingleShoot();
            ShootingHelper.ShootFromPlayer(ProjectileType.DesertEagle, _weaponViewInHand, _projectilesManager);
            
            /*var startingPosition = _weaponViewInHand.GetRandomPoint();
            var controller = new DefaultProjectileController(startingPosition.position, startingPosition.forward);
            var projectile = _projectilesManager.GetOrCreateProjectile(ProjectileType.Default);
            projectile.Init(controller);
            projectile.ToRun(collider =>
            {
                // TODO: доделать
                return collider != null;
            });*/
        }
    }
}