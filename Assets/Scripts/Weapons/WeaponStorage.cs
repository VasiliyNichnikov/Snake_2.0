#nullable enable
using System.Linq;
using Data;
using Snake;
using UnityEngine;

namespace Weapons
{
    public class WeaponStorage : MonoBehaviour, IWeaponVisitor
    {
        [SerializeField] 
        private WeaponView[] _weapons = null!;
        
        private SnakeAnimatorController _animatorController = null!;
        
        private WeaponView? _selectedWeapon;

        public Transform? WeaponMuzzleTransform
        {
            get
            {
                if (_selectedWeapon == null)
                {
                    return null;
                }

                return _selectedWeapon.transform;
            }
        } 
        
        public void Init(SnakeAnimatorController animatorController)
        {
            HideAllWeapons();
            
            _animatorController = animatorController;
            _animatorController.SetWeapon(null);
        }
        
        public IWeaponViewInHand Visit(WeaponDesertEagleData data)
        {
            HideAllWeapons();
            
            return GetAndShowWeaponByType(WeaponType.DesertEagle);
        }

        public IWeaponViewInHand Visit(WeaponKalashData data)
        {
            HideAllWeapons();
            
            return GetAndShowWeaponByType(WeaponType.Kalash);
        }

        public IWeaponViewInHand Visit(WeaponMiniGunData data)
        {
            HideAllWeapons();
            
            return GetAndShowWeaponByType(WeaponType.Minigun);
        }

        private IWeaponViewInHand GetAndShowWeaponByType(WeaponType type)
        {
            var weapon = _weapons.First(w => w.Type == type);
            weapon.Show();
            _animatorController.SetWeapon(type);
            _selectedWeapon = weapon;
            return weapon;
        }
        
        private void HideAllWeapons()
        {
            foreach (var weapon in _weapons)
            {
                weapon.Hide();
            }
        }
    }
}