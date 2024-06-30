#nullable enable
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
        var context = new GameContext(_camera);
        context.AddSnake(_snakeController);
        context.AddSnakePartPrefab(_snakePartControllerPrefab);
        if (_testLevel != null)
        {
            context.AddTestLevel(_testLevel);
        }
        return context;
    }
}
