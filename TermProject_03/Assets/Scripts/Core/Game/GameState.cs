public abstract class GameState : IState
{
    // Constructor
    protected GameState(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }


    // Variables
    protected GameStateManager _gameStateManager;


    // Functions
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}

public enum GameStateKeys
{
    PreparationState,
    WaveState,
    BreakState,
    GameWinState,
    GameOverState
}