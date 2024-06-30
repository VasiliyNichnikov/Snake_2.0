#nullable enable
using Data;
using Projectiles;
using UnityEngine;
using Utils;

namespace Weapons
{
    public class WeaponMiniGunController : IWeaponController
    {
        private readonly WeaponMiniGunData _data;
        private readonly ProjectilesManager _projectilesManager;
        private readonly Timer _timer;

        private bool _isWeaponReloaded = true;
        private IWeaponViewInHand? _weaponViewInHand;
        private IWeaponAnimator? _weaponAnimator;
        

        public WeaponMiniGunController(WeaponMiniGunData data, ProjectilesManager projectilesManager)
        {
            _data = data;
            _timer = new Timer();
            _projectilesManager = projectilesManager;
        }

        public void Shoot()
        {
            if (_weaponViewInHand == null)
            {
                Debug.LogError("WeaponMiniGunController.Shoot: weaponViewInHand is null.");
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
            return new WeaponMiniGunController(_data, _projectilesManager);
        }
    }
}