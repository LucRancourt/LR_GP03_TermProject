using UnityEngine;

using _Project.Code.Core.ServiceLocator;
using UnityEngine.UI;
using _Project.Code.Core.GameManagement;

public class LevelWinState : LevelState
{
    private GameObject _winScreen;
    // Variables
    // GameWin UI
    // Stats Display
    // Replay or Return Options


    public LevelWinState(LevelStateManager levelStateManager) : base(levelStateManager)
    {
        _winScreen = null;
    }


    public override void Enter()
    {
        ServiceLocator.Get<GameManagementService>().TransitionToMenu();

        _winScreen.SetActive(true);
        _winScreen.GetComponentInChildren<Button>().onClick.AddListener(Exit);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _winScreen.SetActive(false);
        ServiceLocator.Get<SceneService>().LoadScene("MainMenu");
    }
}