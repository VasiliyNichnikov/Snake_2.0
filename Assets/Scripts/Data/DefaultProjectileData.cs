#nullable enable
using Projectiles;

namespace Data
{
    public readonly struct DefaultProjectileData
    {
        public readonly ProjectileType Type;
        public readonly float Speed;
        public readonly int Damage;
        public readonly DefaultProjectile Prefab;

        public DefaultProjectileData(ProjectileType type, float speed, int damage, DefaultProjectile prefab)
        {
            Type = type;
            Speed = speed;
            Damage = damage;
            Prefab = prefab;
        }
    }
}