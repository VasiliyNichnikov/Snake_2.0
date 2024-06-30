#nullable enable
using System;
using Enemies;

namespace Snake
{
    public interface IChoosingEnemyTarget
    {
        event Action<IEnemyController> OnSelectedEnemy;
    }
}