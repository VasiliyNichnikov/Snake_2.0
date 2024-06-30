#nullable enable
using UnityEngine;

namespace Enemies
{
    public interface IEnemyController
    {
        Transform Target { get; }
    }
}