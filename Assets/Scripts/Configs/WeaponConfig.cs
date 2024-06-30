#nullable enable
using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "SNT/WeaponConfig", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [Serializable]
        private struct WeaponData
        {
            public string Comment;
            
            [Header("Тип оружия")] 
            public WeaponConfigType Type;
            
            [Header("Скорострельность")]
            public float RateOfFire;

            [Header("Время перезарядки")] 
            public float RechargeTime;

            [Header("Кол-во снарядов в оружие")] 
            public int NumberOfProjectiles;
        }

        [SerializeField] 
        private WeaponData[] Weapons = null!;

        public Data.WeaponData GetData()
        {
            var kalash = new List<WeaponKalashData>();
            var miniGun = new List<WeaponMiniGunData>();
            var desertEagle = new List<WeaponDesertEagleData>();

            foreach (var weapon in Weapons)
            {
                switch (weapon.Type)
                {
                    case WeaponConfigType.DesertEagle:
                        desertEagle.Add(new WeaponDesertEagleData(weapon.RateOfFire, weapon.RechargeTime,
                            weapon.NumberOfProjectiles));
                        break;
                    case WeaponConfigType.Kalash:
                        kalash.Add(new WeaponKalashData(weapon.RateOfFire, weapon.RechargeTime,
                            weapon.NumberOfProjectiles));
                        break;
                    case WeaponConfigType.Minigun:
                        miniGun.Add(new WeaponMiniGunData(weapon.RateOfFire, weapon.RechargeTime,
                            weapon.NumberOfProjectiles));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return new Data.WeaponData(kalash.AsReadOnly(), miniGun.AsReadOnly(), desertEagle.AsReadOnly());
        }
    }
}