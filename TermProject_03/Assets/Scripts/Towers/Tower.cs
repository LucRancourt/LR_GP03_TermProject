using UnityEngine;

public class Tower : MonoBehaviour
{
    // Variables
    [SerializeField] private TowerConfig _towerConfig;


    // Functions
    private void Start()
    {
        Debug.Log(_towerConfig.Name);
        Debug.Log(_towerConfig.UnitLimit);
        Debug.Log(_towerConfig.PlacementCost);
        Debug.Log(_towerConfig.Rate);
        Debug.Log(_towerConfig.SpawnedUnitSpeed);
    }
}
