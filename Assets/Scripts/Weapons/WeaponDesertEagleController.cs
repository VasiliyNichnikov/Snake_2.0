#nullable enable
using Data;

namespace Weapons
{
    public class WeaponDesertEagleController : IWeaponController
    {
        private readonly WeaponDesertEagleData _data;

        public WeaponDesertEagleController(WeaponDesertEagleData data)
        {
            _data = data;
        }

        public void Apply(IWeaponVisitor visitor)
        {
            visitor.Visit(_data);
        }
    }
}