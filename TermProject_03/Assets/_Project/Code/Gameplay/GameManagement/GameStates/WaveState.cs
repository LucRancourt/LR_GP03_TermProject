using UnityEngine;

[GameState(GameStateKeys.WaveState)]
public class WaveState : GameState
{
    // Variables
    // Spawn Enemies periodically
    // Skip Wave option
    // Exit when all wave is dead

    public WaveState(GameStateManager gameStateManager) : base(gameStateManager) { }


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
