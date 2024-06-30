#nullable enable
using Configs;
using Levels;
using Snake;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance { get; private set; } = null!;

    [SerializeField] 
    private Camera _camera = null!;

    [SerializeField] 
    private SnakeController _snakeController = null!;

    [SerializeField] 
    private SnakePartController _snakePartControllerPrefab = null!;

    [SerializeField] 
    private TestLevel? _testLevel;

    [SerializeField] 
    private WeaponConfig _weaponConfig = null!;

    [SerializeField] 
    private ProjectileConfig _projectileConfig = null!;

    [SerializeField] 
    private Transform _projectileParent = null!;
    
    private SnakeGame _game = null!;

    private void Awake()
    {
        Instance = this;
        
        var context = CreateContext();
        _game = new SnakeGame(context);
    }

    private void Start()
    {
        _game.Start();
    }

    private void Update()
    {
        _game.Update();
    }

    private GameContext CreateContext()
    {
        var context = new GameContext(_camera)
                .AddSnake(_snakeController)
                .AddSnakePartPrefab(_snakePartControllerPrefab)
                .AddWeaponConfig(_weaponConfig)
                .AddProjectileConfig(_projectileConfig)
                .AddProjectileParent(_projectileParent)
            ;
        if (_testLevel != null)
        {
            context.AddTestLevel(_testLevel);
        }
        return context;
    }
}
