using UnityEngine;

public class WaveState : GameState
{
    // Constructor
    public WaveState(GameStateManager gameStateManager) : base(gameStateManager) { }

    
    // Variables
    // Spawn Enemies periodically
    // Skip Wave option
    // Exit when all wave is dead


    // Functions
    public override void Enter()
    {
        Debug.Log("WaveState - Enter");
    }

    public override void Execute()
    {
        Debug.Log("WaveState - Execute");
    }

    public override void Exit()
    {
        Debug.Log("WaveState - Exit");
    }
}
