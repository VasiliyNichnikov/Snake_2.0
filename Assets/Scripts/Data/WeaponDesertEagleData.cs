namespace Data
{
    public readonly struct WeaponDesertEagleData
    {
        public readonly float RateOfFire;
        public readonly float RechargeTime;
        public readonly int NumberOfProjectiles;

        public WeaponDesertEagleData(float rateOfFire, float rechargeTime, int numberOfProjectiles)
        {
            RateOfFire = rateOfFire;
            RechargeTime = rechargeTime;
            NumberOfProjectiles = numberOfProjectiles;
        }
    }
}