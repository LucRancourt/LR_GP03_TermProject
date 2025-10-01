using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    // Variables
    [SerializeField] protected TowerConfig _towerConfig;
    private GameObject _towerModel;


    // Functions
    protected virtual void Awake()
    {
        _towerModel = Instantiate(_towerConfig.Model);
        _towerModel.transform.SetParent(transform);
        _towerModel.transform.localPosition = Vector3.zero;

        Debug.Log(_towerConfig.Name);
        Debug.Log(_towerConfig.UnitLimit);
        Debug.Log(_towerConfig.PlacementCost);
        Debug.Log(_towerConfig.Rate);
    }
}
