#nullable enable
using Snake;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] 
    private Camera _camera = null!;

    [SerializeField] 
    private SnakeController _snakeController = null!;

    [SerializeField] 
    private SnakePartController _snakePartControllerPrefab = null!;
    
    private SnakeGame _game = null!;
    
    private void Awake()
    {
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
        return context;
    }
}
