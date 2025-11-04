using UnityEngine;


public class WaveState : LevelState
{
    private WaveManager _waveManager;

    private bool _allWavesCleared;
    // Variables
    // Spawn Enemies periodically
    // Skip Wave option
    // Exit when all wave is dead

    public WaveState(LevelStateManager levelStateManager, WaveManager waveManager) : base(levelStateManager)
    {
        _waveManager = waveManager;
    }


    public override void Enter()
    {
        _allWavesCleared = false;

        _waveManager.WaveCompleted += WaveCompleted;
        _waveManager.AllWavesCompleted += AllWavesCleared;

        _waveManager.StartNextWave();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _waveManager.WaveCompleted -= WaveCompleted;
        _waveManager.AllWavesCompleted -= AllWavesCleared;

        _waveManager.CancelEndWaveCheck();
    }

    private void WaveCompleted()
    {
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
