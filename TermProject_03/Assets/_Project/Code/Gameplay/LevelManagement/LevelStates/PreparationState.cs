using System.Collections;
using UnityEngine;


public class PreparationState : LevelState
{
    private float timeUntilAutoExit = 10.0f;
    // Change Tower Inventory // Do Later

    public PreparationState(LevelStateManager levelStateManager) : base(levelStateManager) { }


    public override void Enter()
    {
        Debug.Log("Prep - Enter");
        CoroutineExecutor.Instance.StartCoroutine(AutoExit());


        // Select Difficulty UI -> send to GameManager?
    }

    public override void Update()
    {
        Debug.Log("Prep - Exec");
    }

    public override void Exit()
    {
        Debug.Log("Prep - Exit");

        CoroutineExecutor.Instance.StopAllCoroutines();
    }


    private IEnumerator AutoExit()
    {
        yield return new WaitForSeconds(timeUntilAutoExit);

        _levelStateManager.TransitionToState<WaveState>();
    }
}