using UnityEngine;

[GameState(GameStateKeys.GameWinState)]
public class GameWinState : GameState
{
    // Constructor
    public GameWinState(GameStateManager gameStateManager) : base(gameStateManager) { }


    // Variables
    // GameWin UI
    // Stats Display
    // Replay or Return Options


    // Functions
    public override void Enter()
    {
        Debug.Log("GameWinState - Enter");
    }

    public override void Update()
    {
        Debug.Log("GameWinState - Execute");
    }

    public override void Exit()
    {
        LevelManager.Instance.LoadLevel("MainMenu");
    }
}