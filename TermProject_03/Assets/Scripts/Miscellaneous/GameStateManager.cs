using UnityEngine;



public class GameStateManager : MonoBehaviour
{
    // Variables
    private StateFactory<GameState, GameStateKeys> _stateFactory = new StateFactory<GameState, GameStateKeys>();
    private GameState _currentState;


    // Functions
    private void Awake()
    {
        //foreach (GameStateKeys key in System.Enum.GetValues(typeof(GameStateKeys)))
        {
            //_stateFactory.RegisterState(key, );
        }

        _stateFactory.RegisterState(GameStateKeys.PreparationState, new PreparationState(this));
        _stateFactory.RegisterState(GameStateKeys.WaveState, new WaveState(this));
        _stateFactory.RegisterState(GameStateKeys.BreakState, new BreakState(this));
        _stateFactory.RegisterState(GameStateKeys.GameWinState, new GameWinState(this));
        _stateFactory.RegisterState(GameStateKeys.GameOverState, new GameOverState(this));

        TransitionToState(GameStateKeys.PreparationState);
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
