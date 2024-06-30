namespace Weapons
{
    public interface IWeaponController
    {
        void Shoot();

        void Update();
        
        void Apply(IWeaponVisitor visitor, IWeaponAnimator animator);

        IWeaponController Clone();
    }
}