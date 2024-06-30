#nullable enable
using System;
using System.Collections.Generic;
using Data;
using Projectiles;
using Snake;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class WeaponsManager
    {
        public IWeaponController? SelectedWeapon { get; private set; }
        
        private readonly WeaponData _data;
        private readonly Action<IWeaponController> _chooseWeapon;
        private readonly ProjectilesManager _projectilesManager;
        
        public WeaponsManager(WeaponData data, ProjectilesManager projectilesManager, Action<IWeaponController> chooseWeapon)
        {
            _data = data;
            _chooseWeapon = chooseWeapon;
            _projectilesManager = projectilesManager;
        }

        public void ChooseWeapon(WeaponType type)
        {
            IWeaponController selectedController;
            switch (type)
            {
                case WeaponType.DesertEagle:
                    var desertEagleData = RandomData(_data.DesertEagle);
                    selectedController = CreateDesertEagleWeapon(desertEagleData);
                    break;
                case WeaponType.Kalash:
                    var kalashData = RandomData(_data.Kalash);
                    selectedController = CreateKalashWeapon(kalashData);
                    break;
                case WeaponType.Minigun:
                    var miniGunData = RandomData(_data.MiniGun);
                    selectedController = CreateMiniGunWeapon(miniGunData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            SelectedWeapon = selectedController;
            _chooseWeapon.Invoke(selectedController);
        }

        private IWeaponController CreateDesertEagleWeapon(WeaponDesertEagleData data)
        {
            return new WeaponDesertEagleController(data, _projectilesManager);
        }

        private IWeaponController CreateKalashWeapon(WeaponKalashData data)
        {
            return new WeaponKalashController(data, _projectilesManager);
        }

        private IWeaponController CreateMiniGunWeapon(WeaponMiniGunData data)
        {
            return new WeaponMiniGunController(data, _projectilesManager);
        }

        private static T RandomData<T>(IReadOnlyList<T> collection) where T : struct
        {
            var result = Random.Range(0, collection.Count - 1);
            return collection[result];
        }
    }
}