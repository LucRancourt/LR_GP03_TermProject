using UnityEngine;

public class GameOverState : GameState
{
    // Constructor
    public GameOverState(GameStateManager gameStateManager) : base(gameStateManager) { }


    // Variables
    // GameOver UI
    // Stats Display
    // Replay or Return Options


    // Functions
    public override void Enter()
    {
        Debug.Log("GameOverState - Enter");
    }

    public override void Execute()
    {
        Debug.Log("GameOverState - Execute");
    }

    public override void Exit()
    {
        LevelManager.Instance.LoadLevel("MainMenu");
    }
}