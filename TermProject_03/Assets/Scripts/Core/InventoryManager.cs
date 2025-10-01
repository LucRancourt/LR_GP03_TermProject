using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Variables
    [SerializeField] private Tower[] towers;


    // Sits on per Player? 
    // Also holds the UI?


    // Functions
    private void Awake()
    {
        
    }

    public Tower GetTower(int slot)
    {
        if (slot < 0 || slot >= towers.Length) Debug.LogError("Non-existent Slot!");

        return towers[slot];
    }
}
