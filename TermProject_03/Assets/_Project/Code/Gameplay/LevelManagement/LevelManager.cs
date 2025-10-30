using UnityEngine;

using _Project.Code.Core.General;


[RequireComponent(typeof(LevelStateManager))]

public class LevelManager : Singleton<LevelManager>
{
    LevelStateManager _levelStateManager;

    // Difficulty set somewhere per level -> not sure where to get from, maybe WaveManager? Idk

    

    private void Start()
    {
        _levelStateManager = GetComponent<LevelStateManager>();

        Base.Instance.Initialize(100.0f);
    }

    public void SetGameOver()
    {
        _levelStateManager.TransitionToState<LevelOverState>();
    }
}
