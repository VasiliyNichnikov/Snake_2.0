#nullable enable
using Snake;
using UnityEngine;

public class GameContext
{
    public readonly Camera Camera;
    public SnakeController SnakeController { get; private set; } = null!;
    public SnakePartController SnakePartControllerPrefab { get; private set; } = null!;
    
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
}