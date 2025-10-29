using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PathNavigator))]
[RequireComponent(typeof(HealthSystem))]

public class Enemy : MonoBehaviour
{
    // Variables
    [SerializeField] private EnemyConfig enemyConfig;

    private PathNavigator _pathNavigator;
    private HealthSystem _healthSystem;

    public event Action<Enemy> OnDiedEvent;


    // Functions
    private void Awake()
    {
        if (enemyConfig == null)
            Debug.LogError("Missing EnemyConfig!");

        Instantiate(enemyConfig.Prefab, transform);

        _pathNavigator = GetComponent<PathNavigator>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    public void Initialize(NavPath path)
    {
        SetupCollider();

        _healthSystem.SetMaxHealth(enemyConfig.Health);
        _healthSystem.OnDiedEvent += OnDied;

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
        _pathNavigator.StopPath();

        OnDiedEvent?.Invoke(this);
    }
}
