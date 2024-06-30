#nullable enable
using System;
using Enemies;

namespace Snake
{
    public interface IChoosingTarget
    {
        event Action<IEnemyController> OnSelectedEnemy;
    }
}