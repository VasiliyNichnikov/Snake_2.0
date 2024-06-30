#nullable enable
using Data;

namespace Weapons
{
    public class WeaponMiniGunController : IWeaponController
    {
        private readonly WeaponMiniGunData _data;

        public WeaponMiniGunController(WeaponMiniGunData data)
        {
            _data = data;
        }

        public void Apply(IWeaponVisitor visitor)
        {
            visitor.Visit(_data);
        }
    }
}