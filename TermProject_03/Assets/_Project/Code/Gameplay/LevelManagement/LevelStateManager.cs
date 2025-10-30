using UnityEngine;

using _Project.Code.Core.StateMachine;

public class LevelStateManager : MonoBehaviour
{
    private StateMachine<LevelState> _smGameStates;


    private void Awake()
    {
        _smGameStates = new StateMachine<LevelState>(new PreparationState(this));
        _smGameStates.AddState(new WaveState(this));
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

    private void Update()
    {
        _smGameStates.Update();
    }
}
