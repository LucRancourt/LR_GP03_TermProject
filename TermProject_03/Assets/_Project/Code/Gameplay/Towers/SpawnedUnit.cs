using UnityEngine;



//  NEEDS TO HAVE A SHARED PARENT? WITH ENEMY - TOO MANY SIMILARITIES



[RequireComponent(typeof(PathNavigator))]
[RequireComponent(typeof(HealthSystem))]

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class SpawnedUnit : MonoBehaviour
{
    // Variables
    [SerializeField] private SpawnedUnitConfig spawnedUnitConfig;
    private GameObject _model;
    private PathNavigator _pathNavigator;
    private HealthSystem _healthSystem;


    // Functions
    private void Awake()
    {
        if (spawnedUnitConfig == null)
            Debug.LogError("Missing SpawnedUnitConfig!");

        _model = Instantiate(spawnedUnitConfig.Model, transform);
        _model.transform.SetParent(transform);
        _model.transform.localPosition = Vector3.zero;

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
        SetupCollider();

        _pathNavigator.SetupPath(path, spawnedUnitConfig.Speed, false);

        _healthSystem.SetMaxHealth(spawnedUnitConfig.Health);
    }

    private void SetupCollider()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        collider.isTrigger = true;
    }

    private void OnEnable()
    {
        _pathNavigator.PlayPath();

        _healthSystem.OnDiedEvent += OnDied;
    }

    private void OnDisable()
    {
        _pathNavigator.StopPath();

        _healthSystem.OnDiedEvent -= OnDied;
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
        //_enemyManager.DespawnEnemy(this);
    }
}
