#nullable enable
namespace Data
{
    public readonly struct WeaponMiniGunData
    {
        public readonly float RateOfFire;
        public readonly float RechargeTime;
        public readonly int NumberOfProjectiles;

        public WeaponMiniGunData(float rateOfFire, float rechargeTime, int numberOfProjectiles)
        {
            RateOfFire = rateOfFire;
            RechargeTime = rechargeTime;
            NumberOfProjectiles = numberOfProjectiles;
        }
    }
}