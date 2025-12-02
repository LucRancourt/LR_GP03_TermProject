using UnityEngine;

using _Project.Code.Core.StateMachine;

public class LevelStateManager : MonoBehaviour
{
    private StateMachine<LevelState> _smGameStates;

    [SerializeField] private WaveManager waveManager;
    [SerializeField] private UIManager uiManager;


    private void Awake()
    {
        _smGameStates = new StateMachine<LevelState>(new PreparationState(this, uiManager.gameObject));// hudManager.Get(HUDItemKey.UnitInventory)));
        _smGameStates.AddState(new WaveState(this, waveManager, uiManager));
        _smGameStates.AddState(new BreakState(this));
        _smGameStates.AddState(new LevelWinState(this, uiManager.Get(UIItemKey.WinScreen)));
        _smGameStates.AddState(new LevelOverState(this, uiManager.Get(UIItemKey.LossScreen)));
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
