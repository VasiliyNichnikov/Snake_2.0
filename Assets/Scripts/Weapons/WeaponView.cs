#nullable enable
using UnityEngine;

namespace Weapons
{
    public class WeaponView : MonoBehaviour, IWeaponViewInHand
    {
        public bool InHand => _inHand;
        public WeaponType Type => _type;

        [SerializeField] private Transform[] _pointsForProjectileDeparture = null!;

        [SerializeField] private WeaponType _type;

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

        public Transform GetRandomPoint()
        {
            if (_pointsForProjectileDeparture == null! || _pointsForProjectileDeparture.Length == 0)
            {
                Debug.LogError("WeaponView.GetRandomPoint: number points is null.");
                return transform;
            }

            if (_pointsForProjectileDeparture.Length == 1)
            {
                return _pointsForProjectileDeparture[0];
            }

            var randomIndex = Random.Range(0, _pointsForProjectileDeparture.Length - 1);
            var point = _pointsForProjectileDeparture[randomIndex];
            return point;
        }
    }
}