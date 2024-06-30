namespace Weapons
{
    public interface IWeaponController
    {
        void Apply(IWeaponVisitor visitor);
    }
}