using UnityEngine;

[GameState(GameStateKeys.GameOverState)]
public class GameOverState : GameState
{
    // Variables
    // GameOver UI
    // Stats Display
    // Replay or Return Options

    public GameOverState(GameStateManager gameStateManager) : base(gameStateManager) { }
    

    public override void Enter()
    {
        Debug.Log("GameOverState - Enter");
    }

    public override void Update()
    {
        Debug.Log("GameOverState - Execute");
    }

    public override void Exit()
    {
        LevelManager.Instance.LoadLevel("MainMenu");
    }
}