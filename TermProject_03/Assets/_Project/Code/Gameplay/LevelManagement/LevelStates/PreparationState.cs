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
        Debug.Log("Prep - Enter");
        _levelStateManager.StartCoroutine(AutoExit());


        // Select Difficulty UI -> send to GameManager?
    }

    public override void Update()
    {
        Debug.Log("Prep - Exec");
    }

    public override void Exit()
    {
        Debug.Log("Prep - Exit");
        _preparationUI.SetActive(false);

        _levelStateManager.StopAllCoroutines();
    }


    private IEnumerator AutoExit()
    {
        yield return new WaitForSeconds(timeUntilAutoExit);

        _levelStateManager.TransitionToState<WaveState>();
    }
}