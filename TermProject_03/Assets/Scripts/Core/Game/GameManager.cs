using UnityEngine;

using _Project.Code.Core.General;


[RequireComponent(typeof(GameStateManager))]

public class GameManager : Singleton<GameManager>
{
    // Variables
    GameStateManager _gameStateManager;

    // Difficulty set somewhere per level -> not sure where to get from, maybe WaveManager? Idk

    // Functions
    private void Start()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        _gameStateManager.TransitionToState(GameStateKeys.PreparationState);

        Base.Instance.Initialize(100.0f);
    }

    public void SetGameOver()
    {
        _gameStateManager.TransitionToState(GameStateKeys.GameOverState);
    }
}
