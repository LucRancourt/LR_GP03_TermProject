using System.Collections;
using UnityEngine;


public class PreparationState : LevelState
{
    private float timeUntilAutoExit = 10.0f;

    private DifficultySelection _difficultySelection;

    // Change Tower Inventory // Do Later


    public PreparationState(LevelStateManager levelStateManager, DifficultySelection difficultySelection) : base(levelStateManager) 
    {
        _difficultySelection = difficultySelection;
    }


    public override void Enter()
    {
        _levelStateManager.Notifier.UpdateDisplay("Please select a difficulty!");

        _difficultySelection.Show();

        Countdown.Instance.StartCountdown((int)timeUntilAutoExit);

        _levelStateManager.CallCoroutineStart(AutoExit());
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _difficultySelection.Hide();

        _levelStateManager.CallCoroutineEnd();
    }


    private IEnumerator AutoExit()
    {
        yield return new WaitForSeconds(timeUntilAutoExit);

        _levelStateManager.TransitionToState<WaveState>();
    }
}