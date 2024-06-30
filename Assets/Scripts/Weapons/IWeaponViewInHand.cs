#nullable enable
using UnityEngine;

namespace Weapons
{
    public interface IWeaponViewInHand
    {
        /// <summary>
        /// Рандомная точка для вылета
        /// </summary>
        Transform GetRandomPoint();
    }
}