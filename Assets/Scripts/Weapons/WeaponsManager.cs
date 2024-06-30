#nullable enable
using System;
using System.Collections.Generic;
using Data;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class WeaponsManager
    {
        public IWeaponController? SelectedWeapon { get; private set; }
        
        private readonly WeaponData _data;
        private readonly Action<IWeaponController> _chooseWeapon;
        
        public WeaponsManager(WeaponData data, Action<IWeaponController> chooseWeapon)
        {
            _data = data;
            _chooseWeapon = chooseWeapon;
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

        private static IWeaponController CreateDesertEagleWeapon(WeaponDesertEagleData data)
        {
            return new WeaponDesertEagleController(data);
        }

        private static IWeaponController CreateKalashWeapon(WeaponKalashData data)
        {
            return new WeaponKalashController(data);
        }

        private static IWeaponController CreateMiniGunWeapon(WeaponMiniGunData data)
        {
            return new WeaponMiniGunController(data);
        }

        private static T RandomData<T>(IReadOnlyList<T> collection) where T : struct
        {
            var result = Random.Range(0, collection.Count - 1);
            return collection[result];
        }
    }
}