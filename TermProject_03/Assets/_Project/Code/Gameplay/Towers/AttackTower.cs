using _Project.Code.Core.Strategy;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : RangedTower
{
    private float _cooldown;

    protected List<Enemy> _enemiesInRange = new List<Enemy>();

    private StrategyExecutor<AttackStrategy, AttackInput> _strategyExecutor;
    [SerializeField] private AttackStrategy[] attackStrategies;
    private AttackInput _attackInput;


    protected override void Initialize()
    {
        base.Initialize();

        if (TowerData.Type != TowerType.Attack) Debug.LogError("Unsupported TowerType!");

        _strategyExecutor = new StrategyExecutor<AttackStrategy, AttackInput>(attackStrategies[0]);
        _strategyExecutor.CurrentStrategy.OnDamageTarget += DamageTarget;
        _attackInput = new AttackInput(transform, transform);
    }

    protected override void Update()
    {
        base.Update();

        _cooldown -= Time.deltaTime;

        if (_cooldown <= 0.0f)
        {
            ClearListOfInactives();

            if (_enemiesInRange.Count != 0)
            {
                if (_enemiesInRange[0].TryGetComponent(out IDamageable damageable))
                {
                    if (_strategyExecutor.CurrentStrategy)
                    {
                        _attackInput.Target = _enemiesInRange[0].transform;
                        _strategyExecutor.CurrentStrategy.Execute(_attackInput);
                    }
                }

                Debug.DrawLine(transform.position, _enemiesInRange[0].transform.position, Color.red, 2.0f);

                _cooldown = TowerData.Rate;
            }
        }
    }

    private void DamageTarget()
    {
        if (_enemiesInRange.Count == 0) return;

        if (_enemiesInRange[0])
        {
            _enemiesInRange[0].OnDamaged(TowerData.Damage);
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
}
