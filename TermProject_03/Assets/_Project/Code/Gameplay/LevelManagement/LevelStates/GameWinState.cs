using _Project.Code.Core.ServiceLocator;
using _Project.Code.Core.GameManagement;

public class LevelWinState : LevelState
{
    private EndScreen _winScreen;

    // Stats Display?


    public LevelWinState(LevelStateManager levelStateManager, EndScreen endScreen) : base(levelStateManager)
    {
        _winScreen = endScreen;
    }


    public override void Enter()
    {
        LevelManager.Instance.DisableTimer();

        ServiceLocator.Get<GameManagementService>().TransitionToMenu();

        _winScreen.UpdateDisplay(true);
        _winScreen.Show();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _winScreen.Hide();
    }
}