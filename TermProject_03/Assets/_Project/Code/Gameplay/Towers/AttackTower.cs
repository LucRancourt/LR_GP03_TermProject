using UnityEngine;

public class AttackTower : RangedTower
{
    // Variables
    private float _cooldown;


    // Functions
    protected override void Awake()
    {
        if (_towerConfig.Type != TowerType.Attack) Debug.LogError("Unsupported TowerType!");

        base.Awake();
    }

    private void Update()
    {
        _cooldown -= Time.deltaTime;

        if (_cooldown <= 0.0f)
        {
            ClearListOfInactives();

            if (_enemiesInRange.Count != 0)
            {
                if (_enemiesInRange[0].TryGetComponent(out IDamageable damageable))
                    damageable.OnDamaged(_towerConfig.Damage);

                Debug.DrawLine(transform.position, _enemiesInRange[0].transform.position, Color.red, 2.0f);

                _cooldown = _towerConfig.Rate;
            }
        }
    }
}
