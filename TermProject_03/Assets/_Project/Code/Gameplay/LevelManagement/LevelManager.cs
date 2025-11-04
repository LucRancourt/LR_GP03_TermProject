using UnityEngine;

using _Project.Code.Core.General;


[RequireComponent(typeof(LevelStateManager))]

public class LevelManager : Singleton<LevelManager>
{
    LevelStateManager _levelStateManager;

    [SerializeField] private Base playerBase;

    // Difficulty set somewhere per level -> not sure where to get from, maybe WaveManager? Idk

    

    private void Start()
    {
        _levelStateManager = GetComponent<LevelStateManager>();
        playerBase.OnDied += SetGameOver;
    }

    public void SetGameOver()
    {
        playerBase.OnDied -= SetGameOver;
        _levelStateManager.TransitionToState<LevelOverState>();
    }
}
