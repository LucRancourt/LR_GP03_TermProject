using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PathNavigator))]
[RequireComponent(typeof(HealthSystem))]

public class Enemy : MonoBehaviour
{
    // Variables
    private EnemyManager _enemyManager;
    [SerializeField] private EnemyConfig enemyConfig;

    private PathNavigator _pathNavigator;
    private HealthSystem _healthSystem;


    // Functions
    private void Awake()
    {
        if (enemyConfig == null)
            Debug.LogError("Missing EnemyConfig!");

        Instantiate(enemyConfig.Prefab, transform);

        _pathNavigator = GetComponent<PathNavigator>();
        _healthSystem = GetComponent<HealthSystem>();
    }

    public void Initialize(EnemyManager enemyManager, NavPath path)
    {
        Setup(enemyManager, path);

        gameObject.SetActive(true);
    }

    private void Setup(EnemyManager enemyManager, NavPath path)
    {
        _enemyManager = enemyManager;

        SetupCollider();

        _pathNavigator.SetupPath(path, enemyConfig.Speed, false);

        _healthSystem.SetMaxHealth(enemyConfig.Health);
        _healthSystem.OnDiedEvent += OnDied;
    }

    private void SetupCollider()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        collider.isTrigger = true;
        collider.includeLayers = LayerMask.NameToLayer("BaseLayer");
        if (collider.includeLayers == -1)
            Debug.LogError("BaseLayer can not be found!");
    }

    private void OnEnable()
    {
        _pathNavigator.PlayPath();
    }

    private void OnDisable()
    {
        _pathNavigator.StopPath();
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
        _enemyManager.DespawnEnemy(this);
    }
}
