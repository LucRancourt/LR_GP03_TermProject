using System;
using UnityEngine;

using _Project.Code.Core.Pool;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PathNavigator))]
[RequireComponent(typeof(HealthSystem))]

public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] private EnemyConfig enemyConfig;

    private PathNavigator _pathNavigator;
    private HealthSystem _healthSystem;

    public event Action<Enemy> OnDiedEvent;



    public void OnCreateForPool()
    {
        if (enemyConfig == null)
            Debug.LogError("Missing EnemyConfig!");

        _pathNavigator = GetComponent<PathNavigator>();
        _healthSystem = GetComponent<HealthSystem>();

        SetupCollider();
    }

    public void OnSpawnFromPool()
    {
        _healthSystem.SetMaxHealth(enemyConfig.Health);
        _healthSystem.OnDiedEvent += OnDied;
    }

    public void Initialize(NavPath path)
    {
        _pathNavigator.SetupPath(path, enemyConfig.Speed, false);
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
            other.GetComponent<HealthSystem>().OnDamaged(_healthSystem.CurrentHealth);
            OnDied();
        }
    }

    private void OnDied()
    {
        OnDiedEvent?.Invoke(this);
    }

    public void OnReturnToPool()
    {
        _healthSystem.OnDiedEvent -= OnDied;
        _pathNavigator.StopPath();
    }
}
