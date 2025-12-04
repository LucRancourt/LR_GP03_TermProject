using UnityEngine;

using _Project.Code.Core.General;


public class LevelManager : Singleton<LevelManager>
{
    LevelStateManager _levelStateManager = new LevelStateManager();

    [SerializeField] private WaveManager waveManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Base playerBase;

    // Difficulty set somewhere per level -> not sure where to get from, maybe WaveManager? Idk


    private void Start()
    {
        waveManager.Initialize();
        _levelStateManager.Initialize(waveManager, uiManager);

        playerBase.OnDied += SetGameOver;

        //uiManager.Get(UIItemKey.Timer);
    }

    public void SetGameOver()
    {
        playerBase.OnDied -= SetGameOver;
        _levelStateManager.TransitionToState<LevelOverState>();
    }

    private void OnDestroy()
    {
        _levelStateManager.CallCoroutineEnd();
    }
}
