using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Variables
    private StateFactory<GameState, GameStateKeys> _stateFactory = new StateFactory<GameState, GameStateKeys>();
    private GameState _currentState;

    // Functions
    private void Awake()
    {
        var gameStateTypes = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(t => t.GetCustomAttribute<GameStateAttribute>() != null);
        

        foreach (var type in gameStateTypes)
        {
            var attribute = type.GetCustomAttribute<GameStateAttribute>();
            var instance = (GameState)Activator.CreateInstance(type, (object)this);
            _stateFactory.RegisterState(attribute.Key, instance);
        }


        /*
        _stateFactory.RegisterState(GameStateKeys.PreparationState, new PreparationState(this));
        _stateFactory.RegisterState(GameStateKeys.WaveState, new WaveState(this));
        _stateFactory.RegisterState(GameStateKeys.BreakState, new BreakState(this));
        _stateFactory.RegisterState(GameStateKeys.GameWinState, new GameWinState(this));
        _stateFactory.RegisterState(GameStateKeys.GameOverState, new GameOverState(this));
        */
    }

    public void TransitionToState(GameStateKeys newGameState)
    {
        _currentState?.Exit();
        _currentState = _stateFactory.GetState(newGameState);
        _currentState?.Enter();
    }

    private void Update()
    {
        _currentState?.Execute();
    }
}
