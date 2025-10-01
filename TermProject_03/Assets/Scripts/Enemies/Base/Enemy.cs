using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]

public class Enemy : MonoBehaviour
{
    // Variables
    private EnemyManager _enemyManager;
    [SerializeField] private EnemyConfig enemyConfig;

    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;


    // Functions
    private void Awake()
    {
        if (enemyConfig == null)
            Debug.LogError("Missing EnemyConfig!");

        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    public void Initialize(EnemyManager enemyManager, NavPath path)
    {
        Setup(enemyManager, path);

        gameObject.SetActive(true);
    }

    private void Setup(EnemyManager enemyManager, NavPath path)
    {
        _enemyManager = enemyManager;

        _enemyMovement.SetupPath(path, enemyConfig.Speed);

        _enemyHealth.SetHealth(enemyConfig.Health);
        _enemyHealth.OnDiedEvent += OnEnemyDied;
    }

    private void OnEnable()
    {
        _enemyMovement.PlayPath();
    }

    private void OnDisable()
    {
        _enemyMovement.StopPath();
    }

    private void OnEnemyDied()
    {
        _enemyManager.DespawnEnemy(this);
    }
}
