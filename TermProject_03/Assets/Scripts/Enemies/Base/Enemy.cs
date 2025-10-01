using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(HealthSystem))]

public class Enemy : MonoBehaviour
{
    // Variables
    private EnemyManager _enemyManager;
    [SerializeField] private EnemyConfig enemyConfig;

    private EnemyMovement _enemyMovement;
    private HealthSystem _healthSystem;


    // Functions
    private void Awake()
    {
        if (enemyConfig == null)
            Debug.LogError("Missing EnemyConfig!");

        Instantiate(enemyConfig.Prefab, transform);

        _enemyMovement = GetComponent<EnemyMovement>();
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

        _enemyMovement.SetupPath(path, enemyConfig.Speed);

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
        _enemyMovement.PlayPath();
    }

    private void OnDisable()
    {
        _enemyMovement.StopPath();
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
