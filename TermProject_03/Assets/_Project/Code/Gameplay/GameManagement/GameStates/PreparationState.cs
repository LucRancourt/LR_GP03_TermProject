using System.Collections;
using UnityEngine;

[GameState(GameStateKeys.PreparationState)]
public class PreparationState : GameState
{
    private float timeUntilAutoExit = 10.0f;
    // Change Tower Inventory // Do Later

    public PreparationState(GameStateManager gameStateManager) : base(gameStateManager) { }


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

        _gameStateManager.TransitionToState<WaveState>();
    }
}