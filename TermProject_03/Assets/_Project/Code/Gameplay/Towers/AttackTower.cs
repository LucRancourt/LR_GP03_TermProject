using UnityEngine;

public class AttackTower : RangedTower
{
    // Variables
    private float _cooldown;


    // Functions
    protected override void Initialize()
    {
        base.Initialize();

        if (towerData.Type != TowerType.Attack) Debug.LogError("Unsupported TowerType!");
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
                    damageable.OnDamaged(towerData.Damage);

                Debug.DrawLine(transform.position, _enemiesInRange[0].transform.position, Color.red, 2.0f);

                _cooldown = towerData.Rate;
            }
        }
    }
}
