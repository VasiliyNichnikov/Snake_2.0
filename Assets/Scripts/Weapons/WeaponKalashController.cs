#nullable enable
using Utils;
using Data;
using Projectiles;
using UnityEngine;

namespace Weapons
{
    public class WeaponKalashController : IWeaponController
    {
        private readonly WeaponKalashData _data;
        private readonly ProjectilesManager _projectilesManager;
        private readonly Timer _timer;
        
        private bool _isWeaponReloaded = true;
        private IWeaponViewInHand? _weaponViewInHand;
        private IWeaponAnimator? _weaponAnimator;
        
        public WeaponKalashController(WeaponKalashData data, ProjectilesManager projectilesManager)
        {
            _data = data;
            _timer = new Timer();
            _projectilesManager = projectilesManager;
        }

        public void Shoot()
        {
            if (_weaponViewInHand == null)
            {
                Debug.LogError("WeaponKalashController.Shoot: weaponViewInHand is null.");
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
            ShootingHelper.ShootFromPlayer(ProjectileType.Kalash, _weaponViewInHand, _projectilesManager);
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
            return new WeaponKalashController(_data, _projectilesManager);
        }
    }
}