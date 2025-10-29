using System.Collections.Generic;

using _Project.Code.Core.Factory;
using _Project.Code.Core.StateMachine;

public class StateFactory<TState, TKey> where TState : IState where TKey : System.Enum, IFactory<TState>
{
    private readonly Dictionary<TKey, TState> _states = new Dictionary<TKey, TState>();


    public void RegisterState(TKey key, TState state)
    {
        _states[key] = state;
    }

    public TState GetState(TKey key)
    {
        if (_states.TryGetValue(key, out TState state))
            return state;

        throw new KeyNotFoundException($"State '{key}' is not registered.");
    }
}
