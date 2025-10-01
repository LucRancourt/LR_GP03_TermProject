using System.Collections.Generic;

public class StateFactory<TState, TKey> where TState : IState where TKey : System.Enum
{
    // Variables
    private readonly Dictionary<TKey, TState> _states = new Dictionary<TKey, TState>();


    // Functions
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
