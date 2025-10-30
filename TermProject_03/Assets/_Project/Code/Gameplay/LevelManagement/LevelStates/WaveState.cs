using UnityEngine;


public class WaveState : LevelState
{
    // Variables
    // Spawn Enemies periodically
    // Skip Wave option
    // Exit when all wave is dead

    public WaveState(LevelStateManager levelStateManager) : base(levelStateManager) { }


    public override void Enter()
    {
        Debug.Log("WaveState - Enter");
    }

    public override void Update()
    {
        Debug.Log("WaveState - Execute");
    }

    public override void Exit()
    {
        Debug.Log("WaveState - Exit");
    }
}
