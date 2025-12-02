using _Project.Code.Core.StateMachine;


public abstract class LevelState : BaseState
{
    protected LevelStateManager _levelStateManager;

    protected LevelState(LevelStateManager gameStateManager)
    {
        _levelStateManager = gameStateManager;
    }
}

public enum LevelStateKeys
{
    PreparationState,
    WaveState,
    BreakState,
    GameWinState,
    GameLossState
}