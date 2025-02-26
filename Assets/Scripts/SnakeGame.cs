﻿#nullable enable
using Data;
using Factories;
using Levels;
using Projectiles;
using Snake;
using UI;

public class SnakeGame
{
    private readonly GameContext _context;
    private readonly StartGamePanel _startGamePanel;
    
    #region Controllers

    private readonly SnakeController _snakeController;
    private readonly ILevelsManager _levelsManager;
    private readonly ProjectilesManager _projectilesManager;
    
    #endregion
    

    public SnakeGame(GameContext context)
    {
        _context = context;
        _snakeController = context.SnakeController;
        var projectileData = context.ProjectileConfig.GetData();
        _projectilesManager = new ProjectilesManager(projectileData, context.ProjectileParent);
        _startGamePanel = context.StartGamePanel;
        
#if UNITY_EDITOR || DEBUG
        var enemiesData = _context.ZombiesConfig.GetEnemiesData();
        if (_context.TestLevel != null)
        {
            _context.TestLevel.Init(enemiesData, _snakeController);
            _levelsManager = new TestLevelsManager(_context.TestLevel);
        }
        else
        {
            if (_context.ProdLevel is PreparedLevel level)
            {
                level.Init(enemiesData, _snakeController);
                _levelsManager = new PreparedLevelsManager(level);
            }
            else
            {
                _levelsManager = new LevelsManager();
            }
        }
#else
        var enemiesData = _context.ZombiesConfig.GetEnemiesData();
        if (_context.ProdLevel is PreparedLevel level)
        {
            level.Init(enemiesData, _snakeController);
            _levelsManager = new PreparedLevelsManager(level);
        }
        else
        {
            _levelsManager = new LevelsManager();
        }
#endif
    }

    public void Start()
    {
        _startGamePanel.Init(() =>
        {
            var weaponData = _context.WeaponConfig.GetData();
            var snakePartFactory = new SnakePartFactory(_context.SnakePartControllerPrefab, _snakeController.transform);
            var data = new SnakeData(_context.Camera, 
                snakePartFactory, 
                weaponData, 
                _projectilesManager);
            _snakeController.Init(data, _levelsManager.GetCurrentLevel());
        });
    }

    public void Update()
    {
        _snakeController.UpdateSnake();
    }
}