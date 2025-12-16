using System.Collections;
using UnityEngine;


public class BreakState : LevelState
{
    private float timeUntilAutoExit = 10.0f;
    private int waveClearedReward = 500;

    public BreakState(LevelStateManager levelStateManager) : base(levelStateManager) { }


    public override void Enter()
    {
        _levelStateManager.CallCoroutineStart(AutoExit());
        PlayerWallet.Instance.AddToWallet(waveClearedReward);

        Countdown.Instance.StartCountdown((int)timeUntilAutoExit);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _levelStateManager.CallCoroutineEnd();
    }


    private IEnumerator AutoExit()
    {
        yield return new WaitForSeconds(timeUntilAutoExit);

        _levelStateManager.TransitionToState<WaveState>();
    }
}