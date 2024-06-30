#nullable enable
using Configs;
using Levels;
using Snake;
using UnityEngine;

public class GameContext
{
    public readonly Camera Camera;
    public SnakeController SnakeController { get; private set; } = null!;
    public SnakePartController SnakePartControllerPrefab { get; private set; } = null!;
    public WeaponConfig WeaponConfig { get; private set; } = null!;
    public ProjectileConfig ProjectileConfig { get; private set; } = null!;
    public TestLevel? TestLevel { get; private set; }
    public Transform ProjectileParent { get; private set; } = null!;
    public ZombiesConfig ZombiesConfig { get; private set; } = null!;
    
    public GameContext(Camera camera)
    {
        Camera = camera;
    }

    public GameContext AddSnake(SnakeController snakeController)
    {
        SnakeController = snakeController;
        return this;
    }

    public GameContext AddSnakePartPrefab(SnakePartController snakePartController)
    {
        SnakePartControllerPrefab = snakePartController;
        return this;
    }

    public GameContext AddWeaponConfig(WeaponConfig weaponConfig)
    {
        WeaponConfig = weaponConfig;
        return this;
    }

    public GameContext AddTestLevel(TestLevel testLevel)
    {
        TestLevel = testLevel;
        return this;
    }

    public GameContext AddProjectileConfig(ProjectileConfig projectileConfig)
    {
        ProjectileConfig = projectileConfig;
        return this;
    }

    public GameContext AddProjectileParent(Transform projectileParent)
    {
        ProjectileParent = projectileParent;
        return this;
    }

    public GameContext AddZombieConfig(ZombiesConfig zombiesConfig)
    {
        ZombiesConfig = zombiesConfig;
        return this;
    }
}