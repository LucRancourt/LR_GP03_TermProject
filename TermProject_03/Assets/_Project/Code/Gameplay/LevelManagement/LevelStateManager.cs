using UnityEngine;

using _Project.Code.Core.StateMachine;
using _Project.Code.Core.General;
using System.Collections;

public class LevelStateManager
{
    private StateMachine<LevelState> _smGameStates;
    private Coroutine _activeCoroutine;
    public PhaseNotifier Notifier { get; private set; }


    public void Initialize(WaveManager waveManager, PhaseNotifier phaseNotifier, WaveCounter waveCounter, WaveSkipper waveSkipper, DifficultySelection difficultySelection)
    {
        Notifier = phaseNotifier;

        _smGameStates = new StateMachine<LevelState>(new PreparationState(this, difficultySelection));// Get(UIItemKey.UnitInventory), uiManager.Get(UIItemKey.DifficultySelect)));
        _smGameStates.AddState(new WaveState(this, waveManager, waveCounter, waveSkipper)); //.Get(UIItemKey.WaveCounter), uiManager.Get(UIItemKey.Notifications)));
        _smGameStates.AddState(new BreakState(this));
        _smGameStates.AddState(new LevelWinState(this));
        _smGameStates.AddState(new LevelOverState(this));
    }

    public void TransitionToState<TState>() where TState : LevelState
    {
        _smGameStates.TransitionTo<TState>();
    }

    public LevelState GetCurrentState()
    {
        return _smGameStates.CurrentState;
    }

    public void UpdateActiveState()
    {
        _smGameStates.Update();
    }

    public void CallCoroutineStart(IEnumerator toCall)
    {
        if (_activeCoroutine != null) { CallCoroutineEnd(); };

        _activeCoroutine = CoroutineExecutor.Instance.StartCoroutineExec(toCall);
    }

    public void CallCoroutineEnd()
    {
        if (_activeCoroutine == null) { return; };

        if (CoroutineExecutor.Instance != null) 
            CoroutineExecutor.Instance.CancelCoroutine(_activeCoroutine);
    }
}
