using UnityEngine;

using _Project.Code.Core.General;


public class LevelManager : Singleton<LevelManager>
{
    LevelStateManager _levelStateManager = new LevelStateManager();

    [Header("General")]
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private Base playerBase;

    [Header("UI Details")]
    [SerializeField] private GameTimer timer;
    [SerializeField] private PhaseNotifier phaseNotifier;
    [SerializeField] private WaveCounter waveCounter;
    [SerializeField] private WaveSkipper waveSkipper;
    [SerializeField] private DifficultySelection difficultySelection;
    [SerializeField] private EndScreen endScreen;


    private void Start()
    {
        _levelStateManager.Initialize(waveManager, phaseNotifier, waveCounter, waveSkipper, difficultySelection, endScreen);

        playerBase.OnDied += SetGameOver;

        timer.IsActive = true;
    }

    private void SetGameOver()
    {
        DisableTimer();

        playerBase.OnDied -= SetGameOver;
        _levelStateManager.TransitionToState<LevelOverState>();
    }

    public void DisableTimer()
    {
        timer.IsActive = false;
    }

    private void OnDestroy()
    {
        _levelStateManager.CallCoroutineEnd();
    }
}
