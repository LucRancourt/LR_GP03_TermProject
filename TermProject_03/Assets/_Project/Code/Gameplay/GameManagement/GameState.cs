using System;

using _Project.Code.Core.StateMachine;


public abstract class GameState : BaseState
{
    protected GameStateManager _gameStateManager;

    protected GameState(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }
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