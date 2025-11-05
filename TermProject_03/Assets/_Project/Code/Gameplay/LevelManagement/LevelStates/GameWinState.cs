using UnityEngine;

using _Project.Code.Core.ServiceLocator;


public class LevelWinState : LevelState
{
    // Variables
    // GameWin UI
    // Stats Display
    // Replay or Return Options


    public LevelWinState(LevelStateManager levelStateManager) : base(levelStateManager) { }


    public override void Enter()
    {
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        ServiceLocator.Get<SceneService>().LoadScene("MainMenu");
    }
}