#nullable enable
using System.Collections.Generic;
using Enemies;

namespace Levels
{
    public interface ILevel
    {
        IReadOnlyCollection<IEnemyController> Enemies { get; }
    }
}