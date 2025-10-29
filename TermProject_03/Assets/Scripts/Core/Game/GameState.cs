using System;

public abstract class GameState : IState
{
    protected GameStateManager _gameStateManager;

    protected GameState(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }


    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}

[AttributeUsage(AttributeTargets.Class)]
public class GameStateAttribute : Attribute
{
    public GameStateKeys Key { get; }
    public GameStateAttribute(GameStateKeys key) => Key = key;
}

public enum GameStateKeys
{
    PreparationState,
    WaveState,
    BreakState,
    GameWinState,
    GameOverState
}