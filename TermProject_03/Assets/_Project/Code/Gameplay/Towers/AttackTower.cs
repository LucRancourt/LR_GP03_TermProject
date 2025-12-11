using _Project.Code.Core.Strategy;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : RangedTower
{
    private float _cooldown;

    protected List<Enemy> _enemiesInRange = new List<Enemy>();

    private StrategyExecutor<BaseAttackStrategy, AttackInput> _strategyExecutor;
    private AttackInput _attackInput;


    protected override void Initialize()
    {
        base.Initialize();

        if (TowerData.Type != TowerType.Attack) Debug.LogError("Unsupported TowerType!");

        _strategyExecutor = new StrategyExecutor<BaseAttackStrategy, AttackInput>(TowerData.GetTowerTierData(0).AttackStrat);
        _attackInput = new AttackInput(transform, _enemiesInRange);
    }

    protected override void Update()
    {
        if (!bHasBeenInitialized) return;

        base.Update();

        _cooldown -= Time.deltaTime;

        ClearListOfInactives();

        if (_cooldown <= 0.0f)
        {
            if (_enemiesInRange.Count != 0)
            {
                if (_enemiesInRange[0].TryGetComponent(out IDamageable damageable))
                {
                    if (_strategyExecutor.CurrentStrategy)
                    {
                        _attackInput.Targets = _enemiesInRange;
                        _strategyExecutor.CurrentStrategy.Execute(_attackInput);
                    }
                }

                Debug.DrawLine(transform.position, _enemiesInRange[0].transform.position, Color.red, 2.0f);

                _cooldown = TowerData.TowerTiers[TowerTier].Cooldown;
            }
        }
    }

    protected override void InitialRaycastOnEnable()
    {
        Collider[] hits = CheckAnyInRange();

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].TryGetComponent(out Enemy newEnemy))
                    if (!_enemiesInRange.Contains(newEnemy))
                        _enemiesInRange.Add(newEnemy);
            }
        }
    }

    protected override void TriggerEnterDetected(Collider other)
    {
        if (other.TryGetComponent(out Enemy newEnemy))
            if (!_enemiesInRange.Contains(newEnemy))
                _enemiesInRange.Add(newEnemy);
    }

    protected override void TriggerExitDetected(Collider other)
    {
        if (other.TryGetComponent(out Enemy newEnemy))
            if (_enemiesInRange.Contains(newEnemy))
                _enemiesInRange.Remove(newEnemy);
    }

    protected void ClearListOfInactives()
    {
        for (int i = _enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (!_enemiesInRange[i].isActiveAndEnabled)
                _enemiesInRange.Remove(_enemiesInRange[i]);
        }
    }

    protected override void UpdateDataOnUpgrade()
    {
        base.UpdateDataOnUpgrade();

        _strategyExecutor.SetStrategy(TowerData.GetTowerTierData(TowerTier).AttackStrat);
    }
}