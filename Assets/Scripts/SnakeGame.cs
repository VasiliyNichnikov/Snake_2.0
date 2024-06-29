#nullable enable
using Data;
using Factories;
using Snake;

public class SnakeGame
{
    private readonly GameContext _context;

    #region Controllers

    private readonly SnakeController _snakeController;

    #endregion
    
    public SnakeGame(GameContext context)
    {
        _context = context;
        _snakeController = context.SnakeController;
    }

    public void Start()
    {
        var snakePartFactory = new SnakePartFactory(_context.SnakePartControllerPrefab, _snakeController.transform);
        var data = new SnakeData(_context.Camera, snakePartFactory);
        _snakeController.Init(data);
    }

    public void Update()
    {
        _snakeController.UpdateSnake();
    }
}