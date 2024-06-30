#nullable enable
using UnityEngine;

namespace Enemies
{
    public abstract class EnemyControllerBase : MonoBehaviour, IEnemyController
    {
        public Transform Target => transform;
    }
}