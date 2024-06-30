#nullable enable
using System.Collections.ObjectModel;

namespace Data
{
    public readonly struct WeaponData
    {
        public readonly ReadOnlyCollection<WeaponKalashData> Kalash;
        public readonly ReadOnlyCollection<WeaponMiniGunData> MiniGun;
        public readonly ReadOnlyCollection<WeaponDesertEagleData> DesertEagle;

        public WeaponData(ReadOnlyCollection<WeaponKalashData> kalash, ReadOnlyCollection<WeaponMiniGunData> miniGun, ReadOnlyCollection<WeaponDesertEagleData> desertEagle)
        {
            Kalash = kalash;
            MiniGun = miniGun;
            DesertEagle = desertEagle;
        }
    }
}