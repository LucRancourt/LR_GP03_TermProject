using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

using _Project.Code.Core.StateMachine;
using _Project.Code.Core.Factory;

public class GameStateManager : MonoBehaviour
{
    // Variables
    //private StateFactory<GameState, GameStateKeys> _stateFactory = new StateFactory<GameState, GameStateKeys>();
    private FiniteStateMachine<GameState> _fsmGameStates;
    private GameState _currentState;

    // Functions
    private void Awake()
    {

        var gameStateTypes = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(t => t.GetCustomAttribute<GameStateAttribute>() != null);

        foreach (var type in gameStateTypes)
        {
            if (_fsmGameStates == null)
            {
                _fsmGameStates = new FiniteStateMachine<GameState>((GameState)Activator.CreateInstance(type, (object)this));
                continue;
            }

            //var attribute = type.GetCustomAttribute<GameStateAttribute>();
            var instance = (GameState)Activator.CreateInstance(type, (object)this);
            _fsmGameStates.AddState(instance);
        }


        /*
        _stateFactory.RegisterState(GameStateKeys.PreparationState, new PreparationState(this));
        _stateFactory.RegisterState(GameStateKeys.WaveState, new WaveState(this));
        _stateFactory.RegisterState(GameStateKeys.BreakState, new BreakState(this));
        _stateFactory.RegisterState(GameStateKeys.GameWinState, new GameWinState(this));
        _stateFactory.RegisterState(GameStateKeys.GameOverState, new GameOverState(this));
        */
    }

    public void TransitionToState<TState>() where TState : GameState
    {
        _fsmGameStates.TransitionTo<TState>();
    }

    private void Update()
    {
        _fsmGameStates.Update();
    }
}
