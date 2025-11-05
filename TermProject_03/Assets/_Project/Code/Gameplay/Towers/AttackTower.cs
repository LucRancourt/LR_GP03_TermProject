using System.Collections.Generic;
using UnityEngine;

public class AttackTower : RangedTower
{
    private float _cooldown;

    protected List<Enemy> _enemiesInRange = new List<Enemy>();


    protected override void Initialize()
    {
        base.Initialize();

        if (towerData.Type != TowerType.Attack) Debug.LogError("Unsupported TowerType!");
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
                    damageable.OnDamaged(towerData.Damage);

                Debug.DrawLine(transform.position, _enemiesInRange[0].transform.position, Color.red, 2.0f);

                _cooldown = towerData.Rate;
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
}
