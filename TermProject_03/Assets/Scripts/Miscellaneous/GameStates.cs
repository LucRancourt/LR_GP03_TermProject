using UnityEngine;

public abstract class GameState : IState
{
    // Variables
    protected GameStateManager _gameStateManager;


    // Constructor
    protected GameState(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }


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



public class PreparationState : GameState
{
    // Constructor
    public PreparationState(GameStateManager gameStateManager) : base(gameStateManager) { }
    
    // Variables
    
    // Change Tower Inventory // Do Later

    public override void Enter()
    {
        Debug.Log("Prep - Enter");

        // Select Difficulty UI -> send to GameManager?
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        Debug.Log("Prep - Exec");
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        Debug.Log("Prep - Exit");

        _gameStateManager.TransitionToState(GameStateKeys.WaveState);
    }
}

public class BreakState : GameState
{
    public BreakState(GameStateManager gameStateManager) : base(gameStateManager) { }

    // Give money based on Difficulty / Wave Number
    // Time until automatically exits

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        _gameStateManager.TransitionToState(GameStateKeys.WaveState);
    }
}

public class WaveState : GameState
{
    public WaveState(GameStateManager gameStateManager) : base(gameStateManager) { }

    // Spawn Enemies periodically
    // Skip Wave option
    // Exit when all wave is dead

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}

public class GameWinState : GameState
{
    public GameWinState(GameStateManager gameStateManager) : base(gameStateManager) { }

    // GameWin UI
    // Stats Display
    // Replay or Return Options

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}

public class GameOverState : GameState
{
    public GameOverState(GameStateManager gameStateManager) : base(gameStateManager) { }

    // GameOver UI
    // Stats Display
    // Replay or Return Options

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}