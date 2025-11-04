using System.Collections.Generic;

using _Project.Code.Core.Factory;
using UnityEngine;

public class TowerManager
{
    private class LimitedPool
    {
        public int MaxUnitLimit { get; private set; }
        public int CurrentUnitLimit { get; private set; }
        public PooledFactory<Tower> Pool { get; private set; }

        public LimitedPool(int unitLimit, PooledFactory<Tower> towerPool)
        {
            MaxUnitLimit = unitLimit;
            Pool = towerPool;

            CurrentUnitLimit = 0;
        }

        public void AddToCurrentLimit() => CurrentUnitLimit++;
        public void SubtractFromCurrentLimit() => CurrentUnitLimit--;
    }

    private Dictionary<string, LimitedPool> _towerPoolFactory = new();

    public TowerManager(TowerData[] playerInventory)   
    {
        foreach (TowerData tower in playerInventory)
        {
            _towerPoolFactory[tower.Name] = new LimitedPool(tower.UnitLimit, new PooledFactory<Tower>(tower.Model, tower.UnitLimit));
        }
    }

    public Tower SpawnTower(TowerData towerData)
    {
        Debug.Log("MAX " + _towerPoolFactory[towerData.Name].MaxUnitLimit);
        Debug.Log("CUL " + _towerPoolFactory[towerData.Name].CurrentUnitLimit);
        if (_towerPoolFactory[towerData.Name].CurrentUnitLimit < _towerPoolFactory[towerData.Name].MaxUnitLimit)
        {
            Tower tower = _towerPoolFactory[towerData.Name].Pool.Create();
            _towerPoolFactory[towerData.Name].AddToCurrentLimit();

            tower.Initialize(towerData);

            tower.OnDespawned += DespawnTower;

            return tower;
        }

        return null;
    }

    public bool TrySpawnTower(TowerData towerData, out Tower towerSpawned)
    {
        if (_towerPoolFactory[towerData.Name].CurrentUnitLimit < _towerPoolFactory[towerData.Name].MaxUnitLimit)
        {
            towerSpawned = _towerPoolFactory[towerData.Name].Pool.Create();
            _towerPoolFactory[towerData.Name].AddToCurrentLimit();

            towerSpawned.Initialize(towerData);

            towerSpawned.OnDespawned += DespawnTower;

            return true;
        }

        towerSpawned = null;
        return false;
    }

    public void DespawnTower(Tower towerToDespawn, TowerData towerData)
    {
        towerToDespawn.OnDespawned -= DespawnTower;

        _towerPoolFactory[towerData.Name].Pool.Return(towerToDespawn);
        _towerPoolFactory[towerData.Name].SubtractFromCurrentLimit();
    }
}