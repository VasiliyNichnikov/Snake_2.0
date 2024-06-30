#nullable enable
namespace Data
{
    public readonly struct WeaponKalashData
    {
        public readonly float RateOfFire;
        public readonly float RechargeTime;
        public readonly int NumberOfProjectiles;

        public WeaponKalashData(float rateOfFire, float rechargeTime, int numberOfProjectiles)
        {
            RateOfFire = rateOfFire;
            RechargeTime = rechargeTime;
            NumberOfProjectiles = numberOfProjectiles;
        }
    }
}