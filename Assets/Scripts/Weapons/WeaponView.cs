#nullable enable
using UnityEngine;

namespace Weapons
{
    public class WeaponView : MonoBehaviour, IWeaponViewInHand
    {
        public bool InHand => _inHand;
        public WeaponType Type => _type;
        
        [SerializeField] 
        private Transform[] _pointsForProjectileDeparture = null!;
        
        [SerializeField]
        private WeaponType _type;

        [SerializeField, Header("Костыль, обязательно, если оружие на руках юнита")] 
        private bool _inHand;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Подобрать оружие
        /// Отличие от Hide, что мы его удаляем с карты
        /// </summary>
        public void PickUp()
        {
            Hide();
            Destroy(gameObject);
        }
    }
}