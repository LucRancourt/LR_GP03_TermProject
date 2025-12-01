using System.Collections.Generic;

using _Project.Code.Core.Factory;

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

    public TowerManager(BaseTowerData[] playerInventory)   
    {
        foreach (BaseTowerData tower in playerInventory)
        {
            _towerPoolFactory[tower.Name] = new LimitedPool(tower.UnitLimit, new PooledFactory<Tower>(tower.GetTowerTierData(0).Model, tower.UnitLimit));
        }
    }

    public Tower SpawnTower(BaseTowerData towerData)
    {
        if (_towerPoolFactory[towerData.Name].CurrentUnitLimit < _towerPoolFactory[towerData.Name].MaxUnitLimit)
        {
            Tower tower = _towerPoolFactory[towerData.Name].Pool.Create();
            _towerPoolFactory[towerData.Name].AddToCurrentLimit();

            tower.Initialize(towerData);

            return tower;
        }

        return null;
    }

    public bool TrySpawnTower(BaseTowerData towerData, out Tower towerSpawned)
    {
        if (_towerPoolFactory[towerData.Name].CurrentUnitLimit < _towerPoolFactory[towerData.Name].MaxUnitLimit)
        {
            towerSpawned = _towerPoolFactory[towerData.Name].Pool.Create();
            _towerPoolFactory[towerData.Name].AddToCurrentLimit();

            towerSpawned.Initialize(towerData);

            return true;
        }

        towerSpawned = null;
        return false;
    }

    public void DespawnTower(Tower towerToDespawn)
    {
        _towerPoolFactory[towerToDespawn.TowerData.Name].Pool.Return(towerToDespawn);
        _towerPoolFactory[towerToDespawn.TowerData.Name].SubtractFromCurrentLimit();
    }

    public int GetCurrentUnitLimit(BaseTowerData towerData)
    {
        return _towerPoolFactory[towerData.Name].CurrentUnitLimit;
    }
}