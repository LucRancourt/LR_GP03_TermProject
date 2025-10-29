using System.Collections;
using UnityEngine;

[GameState(GameStateKeys.BreakState)]
public class BreakState : GameState
{
    private float timeUntilAutoExit = 10.0f;
    // Give money based on Difficulty / Wave Number

    public BreakState(GameStateManager gameStateManager) : base(gameStateManager) { }


    public override void Enter()
    {
        Debug.Log("Break - Enter");

        CoroutineExecutor.Instance.StartCoroutine(AutoExit());
    }

    public override void Update()
    {
        Debug.Log("Break - Execute");
    }

    public override void Exit()
    {
        Debug.Log("Break - Exit");

        CoroutineExecutor.Instance.StopAllCoroutines();
    }


    private IEnumerator AutoExit()
    {
        yield return new WaitForSeconds(timeUntilAutoExit);

        _gameStateManager.TransitionToState<WaveState>();
    }
}