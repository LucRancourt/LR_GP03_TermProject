using System;
using UnityEngine;

using _Project.Code.Core.Pool;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PathNavigator))]
[RequireComponent(typeof(HealthSystem))]

public class Enemy : MonoBehaviour, IPoolable
{
    public string Name { get; private set; }

    private PathNavigator _pathNavigator;
    private HealthSystem _healthSystem;

    public event Action<Enemy> OnDiedEvent;



    public void OnCreateForPool()
    {
        _pathNavigator = GetComponent<PathNavigator>();
        _healthSystem = GetComponent<HealthSystem>();

        SetupCollider();
    }

    public void OnSpawnFromPool()
    {
    }

    public void Initialize(EnemyData data, NavPath path)
    {
        Name = data.Name;

        _healthSystem.SetMaxHealth(data.Health);
        _healthSystem.OnDiedEvent += OnDied;

        _pathNavigator.SetupPath(path, data.Speed, false);
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
