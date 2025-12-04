using System.Collections;
using UnityEngine;


public class PreparationState : LevelState
{
    private float timeUntilAutoExit = 10.0f;
    private float _currentTimeUntilAutoExit;

    private GameObject _preparationUI;
    // Change Tower Inventory // Do Later

    public PreparationState(LevelStateManager levelStateManager, GameObject preparationUI) : base(levelStateManager) 
    {
        _preparationUI = preparationUI;
    }


    public override void Enter()
    {
        _preparationUI.SetActive(true);

        _levelStateManager.CallCoroutineStart(AutoExit());


        // Select Difficulty UI -> send to GameManager?
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _preparationUI.SetActive(false);

        _levelStateManager.CallCoroutineEnd();
    }


    private IEnumerator AutoExit()
    {
        yield return new WaitForSeconds(timeUntilAutoExit);

        _levelStateManager.TransitionToState<WaveState>();
    }
}