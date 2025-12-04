using _Project.Code.Core.General;
using System.Collections;
using UnityEngine;


public class WaveState : LevelState
{
    private WaveManager _waveManager;
    private UIManager _hudManager;
    private bool _allWavesCleared;

    private WaitForSeconds _delayBeforeStart = new WaitForSeconds(3.0f);
    private WaitForSeconds _delayAfterEnd = new WaitForSeconds(3.0f);

    // Skip Wave option

    public WaveState(LevelStateManager levelStateManager, WaveManager waveManager, UIManager hudManager) : base(levelStateManager)
    {
        _waveManager = waveManager;
        _hudManager = hudManager;
    }

    public override void Enter()
    {
        _allWavesCleared = false;

        //hud set new wave notif

        CoroutineExecutor.Instance.StartCoroutineExec(StartWaveDelay());
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _waveManager.OnWaveCompleted -= WaveCompleted;
        _waveManager.OnAllWavesCompleted -= AllWavesCleared;

        _waveManager.CancelEndWaveCheck();
    }


    IEnumerator StartWaveDelay()
    {
        yield return _delayBeforeStart;

        _waveManager.OnWaveCompleted += WaveCompleted;
        _waveManager.OnAllWavesCompleted += AllWavesCleared;

        _waveManager.StartNextWave();

        // hud open skip option
    }


    private void WaveCompleted()
    {
        // hud wave cleared notif

        CoroutineExecutor.Instance.StartCoroutineExec(WaveCompleteDelay());
    }

    IEnumerator WaveCompleteDelay()
    {
        yield return _delayAfterEnd;

        if (_allWavesCleared)
            _levelStateManager.TransitionToState<LevelWinState>();
        else
            _levelStateManager.TransitionToState<BreakState>();
    }

    private void AllWavesCleared()
    {
        _allWavesCleared = true;
    }
}