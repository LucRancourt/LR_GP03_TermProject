using System.Collections;
using UnityEngine;


public class BreakState : LevelState
{
    private float timeUntilAutoExit = 10.0f;
    // Give money based on Difficulty / Wave Number

    public BreakState(LevelStateManager levelStateManager) : base(levelStateManager) { }


    public override void Enter()
    {
        _levelStateManager.StartCoroutine(AutoExit());
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _levelStateManager.StopAllCoroutines();
    }


    private IEnumerator AutoExit()
    {
        yield return new WaitForSeconds(timeUntilAutoExit);

        _levelStateManager.TransitionToState<WaveState>();
    }
}