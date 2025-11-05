using UnityEngine;

using _Project.Code.Core.StateMachine;

public class LevelStateManager : MonoBehaviour
{
    private StateMachine<LevelState> _smGameStates;

    [SerializeField] private WaveManager waveManager;
    [SerializeField] private HUDManager hudManager;


    private void Awake()
    {
        _smGameStates = new StateMachine<LevelState>(new PreparationState(this, hudManager.gameObject));// hudManager.Get(HUDItemKey.UnitInventory)));
        _smGameStates.AddState(new WaveState(this, waveManager));
        _smGameStates.AddState(new BreakState(this));
        _smGameStates.AddState(new LevelWinState(this, hudManager.Get(HUDItemKey.WinScreen)));
        _smGameStates.AddState(new LevelOverState(this, hudManager.Get(HUDItemKey.LossScreen)));
    }

    public void TransitionToState<TState>() where TState : LevelState
    {
        _smGameStates.TransitionTo<TState>();
    }

    public LevelState GetCurrentState()
    {
        return _smGameStates.CurrentState;
    }

    private void Update()
    {
        _smGameStates.Update();
    }
}
