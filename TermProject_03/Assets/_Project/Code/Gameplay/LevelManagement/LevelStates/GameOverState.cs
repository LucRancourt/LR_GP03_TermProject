using UnityEngine;

using _Project.Code.Core.ServiceLocator;
using UnityEngine.UI;
using _Project.Code.Core.GameManagement;

public class LevelOverState : LevelState
{
    private GameObject _lossScreen;
    // Variables
    // GameOver UI
    // Stats Display
    // Replay or Return Options

    public LevelOverState(LevelStateManager levelStateManager) : base(levelStateManager)
    {
        _lossScreen = null;
    }
    

    public override void Enter()
    {
        ServiceLocator.Get<GameManagementService>().TransitionToMenu();
        _lossScreen.SetActive(true);
        _lossScreen.GetComponentInChildren<Button>().onClick.AddListener(Exit);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _lossScreen.SetActive(false);
        ServiceLocator.Get<SceneService>().LoadScene("MainMenu");
    }
}