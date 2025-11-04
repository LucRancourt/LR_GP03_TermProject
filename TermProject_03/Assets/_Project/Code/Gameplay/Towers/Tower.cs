using _Project.Code.Core.Pool;
using System;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IPoolable, IClickable
{
    // Variables
    protected TowerData towerData;
    private bool _hasBeenInitialized = false;
    private GameObject _towerModel;

    public event Action<Tower, TowerData> OnDespawned;
    private GameObject _spaceTakenRange;
    //private MeshRenderer _rangeMesh;


    // Functions
    protected virtual void Awake()
    {/*
        _towerModel = Instantiate(towerData.Model);
        _towerModel.transform.SetParent(transform);
        _towerModel.transform.localPosition = Vector3.zero;*/
    }

    public virtual void ShowVisuals()
    {
    }

    public virtual void HideVisuals()
    {
    }

    public void Initialize(TowerData data)
    {
        towerData = data;

        if (_hasBeenInitialized) return;

        Initialize();

        _hasBeenInitialized = true;

        OnEnabled();
    }

    protected abstract void Initialize();

    public void DespawnTower()
    {
        OnDespawned?.Invoke(this, towerData);
    }

    protected virtual void OnEnabled() { }
    protected virtual void OnDisabled() { }

    public void OnCreateForPool() { }
    public void OnSpawnFromPool()
    {
        if (_hasBeenInitialized)
            OnEnabled();
    }

    public void OnReturnToPool()
    {
        if (_hasBeenInitialized)
            OnDisabled();
    }
}
