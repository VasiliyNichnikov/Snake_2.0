#nullable enable
using System.Collections.ObjectModel;

namespace Data
{
    public readonly struct ProjectileData
    {
        public readonly ReadOnlyCollection<DefaultProjectileData> DefaultProjectiles;

        public ProjectileData(ReadOnlyCollection<DefaultProjectileData> defaultProjectiles)
        {
            DefaultProjectiles = defaultProjectiles;
        }
    }
}