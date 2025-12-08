using _Project.Code.Core.General;
using System.Collections;
using UnityEngine;


public class WaveState : LevelState
{
    private WaveManager _waveManager;

    private WaitForSeconds _delayBeforeStart = new WaitForSeconds(3.0f);
    private WaitForSeconds _delayAfterEnd = new WaitForSeconds(3.0f);

    private WaveCounter _waveCounter;
    private WaveSkipper _waveSkipper;

    private bool _isFirstEntry = true;


    public WaveState(LevelStateManager levelStateManager, WaveManager waveManager, WaveCounter waveCounter, WaveSkipper waveSkipper) : base(levelStateManager)
    {
        _waveManager = waveManager;
        _waveCounter = waveCounter;
        _waveSkipper = waveSkipper;
    }

    public override void Enter()
    {
        if (_isFirstEntry)
        {
            _waveManager.Initialize();
            _isFirstEntry = false;
        }

        _levelStateManager.Notifier.UpdateDisplay("Wave Starting!");

        CoroutineExecutor.Instance.StartCoroutineExec(StartWaveDelay());
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _waveManager.OnWaveCompleted -= WaveCompleted;

        _waveManager.CancelEndWaveCheck();
    }


    IEnumerator StartWaveDelay()
    {
        yield return _delayBeforeStart;

        _waveManager.OnWaveCompleted += WaveCompleted;

        _waveManager.StartNextWave();

        _waveCounter.IncrementWaveCount();

        if (!_waveManager.AreAllWavesSpawned())
        {
            _waveSkipper.OnWaveSkipped += WaveCompleted;
            _waveSkipper.Show();
        }
    }


    private void WaveCompleted()
    {
        _waveSkipper.Hide();
        _waveSkipper.OnWaveSkipped -= WaveCompleted;

        _levelStateManager.Notifier.UpdateDisplay($"Wave {_waveCounter.CurrentWave} Cleared!");

        CoroutineExecutor.Instance.StartCoroutineExec(WaveCompleteDelay());
    }

    IEnumerator WaveCompleteDelay()
    {
        yield return _delayAfterEnd;

        if (_waveManager.AreAllWavesSpawned())
            _levelStateManager.TransitionToState<LevelWinState>();
        else
            _levelStateManager.TransitionToState<BreakState>();
    }
}