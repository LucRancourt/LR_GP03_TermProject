using System.Collections;
using UnityEngine;

public class PreparationState : GameState
{
    // Constructor
    public PreparationState(GameStateManager gameStateManager) : base(gameStateManager) { }


    // Variables
    [SerializeField] private float timeUntilAutoExit = 10.0f;
        // Change Tower Inventory // Do Later


    // Functions
    public override void Enter()
    {
        Debug.Log("Prep - Enter");
        CoroutineExecutor.Instance.StartCoroutine(AutoExit());


        // Select Difficulty UI -> send to GameManager?
    }

    public override void Execute()
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

        _gameStateManager.TransitionToState(GameStateKeys.WaveState);
    }
}