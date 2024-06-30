#nullable enable
using Levels;
using Snake;
using UnityEngine;

public class GameContext
{
    public readonly Camera Camera;
    public SnakeController SnakeController { get; private set; } = null!;
    public SnakePartController SnakePartControllerPrefab { get; private set; } = null!;
    
    public TestLevel? TestLevel { get; private set; }
    
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

    public GameContext AddTestLevel(TestLevel testLevel)
    {
        TestLevel = testLevel;
        return this;
    }
}