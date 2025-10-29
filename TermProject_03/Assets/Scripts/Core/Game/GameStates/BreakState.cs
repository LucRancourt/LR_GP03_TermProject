using System.Collections;
using UnityEngine;

public class BreakState : GameState
{
    // Constructor
    public BreakState(GameStateManager gameStateManager) : base(gameStateManager) { }


    // Variables
    [SerializeField] private float timeUntilAutoExit = 10.0f;
        // Give money based on Difficulty / Wave Number


    // Functions
    public override void Enter()
    {
        Debug.Log("Break - Enter");

        CoroutineExecutor.Instance.StartCoroutine(AutoExit());
    }

    public override void Execute()
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

        _gameStateManager.TransitionToState(GameStateKeys.WaveState);
    }
}