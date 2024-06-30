#nullable enable
using Data;
using Factories;
using Levels;
using Snake;

public class SnakeGame
{
    private readonly GameContext _context;

    #region Controllers

    private readonly SnakeController _snakeController;
    private readonly ILevelsManager _levelsManager;
    
    #endregion
    

    public SnakeGame(GameContext context)
    {
        _context = context;
        _snakeController = context.SnakeController;
        
#if UNITY_EDITOR || DEBUG
        if (_context.TestLevel != null)
        {
            _levelsManager = new TestLevelsManager(_context.TestLevel);
        }
        else
        {
            _levelsManager = new LevelsManager();
        }
#else
        _levelsManager = new LevelsManager();
#endif
    }

    public void Start()
    {
        var weaponData = _context.WeaponConfig.GetData();
        var snakePartFactory = new SnakePartFactory(_context.SnakePartControllerPrefab, _snakeController.transform);
        var data = new SnakeData(_context.Camera, snakePartFactory, weaponData);
        _snakeController.Init(data, _levelsManager.GetCurrentLevel());
    }

    public void Update()
    {
        _snakeController.UpdateSnake();
    }
}