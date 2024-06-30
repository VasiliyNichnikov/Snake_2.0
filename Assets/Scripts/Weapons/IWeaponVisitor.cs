#nullable enable
using Data;

namespace Weapons
{
    public interface IWeaponVisitor
    {
        IWeaponViewInHand Visit(WeaponDesertEagleData data);
        
        IWeaponViewInHand Visit(WeaponKalashData data);
        
        IWeaponViewInHand Visit(WeaponMiniGunData data);
    }
}