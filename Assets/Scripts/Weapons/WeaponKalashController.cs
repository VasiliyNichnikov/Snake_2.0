#nullable enable
using Data;

namespace Weapons
{
    public class WeaponKalashController : IWeaponController
    {
        private readonly WeaponKalashData _data;
        
        public WeaponKalashController(WeaponKalashData data)
        {
            _data = data;
        }

        public void Apply(IWeaponVisitor visitor)
        {
            visitor.Visit(_data);
        }
    }
}