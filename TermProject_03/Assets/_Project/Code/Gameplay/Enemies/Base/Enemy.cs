using System;
using UnityEngine;

using _Project.Code.Core.Pool;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PathNavigator))]

public class Enemy : BaseDamageable, IPoolable
{
    public string Name { get; private set; }
    public int Value { get; private set; }
    public float Speed { get; private set; }

    private PathNavigator _pathNavigator;

    public event Action<Enemy> OnEnemyDiedEvent;


    public void OnCreateForPool()
    {
        _pathNavigator = GetComponent<PathNavigator>();

        SetupHealthSystem();
        SetupCollider();
    }

    public void OnSpawnFromPool()
    {
    }

    public void Initialize(EnemyData data, NavPath path)
    {
        Name = data.Name;

        SetHealthDefaults(data.Health);
        OnDied += OnEnemyDied;

        Value = data.Value;

        Speed = data.Speed;
        _pathNavigator.SetupPath(path, Speed, false);
        _pathNavigator.PlayPath();
    }

    private void SetupCollider()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        collider.isTrigger = true;
        collider.includeLayers = LayerMask.NameToLayer("BaseLayer");
        if (collider.includeLayers == -1)
            Debug.LogError("BaseLayer can not be found!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Base>())
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(GetCurrentHealth());
                OnEnemyDied();
            }
        }
    }

    private void OnEnemyDied()
    {
        OnEnemyDiedEvent?.Invoke(this);
    }

    public void OnReturnToPool()
    {
        ResetHealthDefaults();
        OnDied -= OnEnemyDied;

        _pathNavigator.StopPath();
    }

    public void UpdateSpeed(float speedAdjustment) { _pathNavigator.SetSpeed(Speed * speedAdjustment);  }
}
