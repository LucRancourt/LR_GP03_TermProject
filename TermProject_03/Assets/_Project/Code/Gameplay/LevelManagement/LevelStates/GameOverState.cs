using _Project.Code.Core.ServiceLocator;
using _Project.Code.Core.GameManagement;


public class LevelOverState : LevelState
{
    private EndScreen _lossScreen;

    // Stats Display


    public LevelOverState(LevelStateManager levelStateManager, EndScreen endScreen) : base(levelStateManager)
    {
        _lossScreen = endScreen;
    }
    

    public override void Enter()
    {
        ServiceLocator.Get<GameManagementService>().TransitionToMenu();

        _lossScreen.UpdateDisplay(false);
        _lossScreen.Show();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _lossScreen.Hide();
    }
}