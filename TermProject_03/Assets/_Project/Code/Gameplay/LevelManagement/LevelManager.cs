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


    protected override void Awake()
    {
        base.Awake();

        _levelStateManager.Initialize(waveManager, phaseNotifier, waveCounter, waveSkipper, difficultySelection);
    }

    private void Start()
    {
        waveManager.Initialize();

        playerBase.OnDied += SetGameOver;

        timer.IsActive = true;
    }

    public void SetGameOver()
    {
        timer.IsActive = false;

        playerBase.OnDied -= SetGameOver;
        _levelStateManager.TransitionToState<LevelOverState>();
    }

    private void OnDestroy()
    {
        _levelStateManager.CallCoroutineEnd();
    }
}
