using UnityEngine;

using _Project.Code.Core.ServiceLocator;


public class LevelOverState : LevelState
{
    // Variables
    // GameOver UI
    // Stats Display
    // Replay or Return Options

    public LevelOverState(LevelStateManager levelStateManager) : base(levelStateManager) { }
    

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
        ServiceLocator.Get<SceneService>().LoadScene("MainMenu");
    }
}