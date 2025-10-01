using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Variables
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private NavPath path;

    private EnemyManager _enemyManager;

    // Functions
    private void Start()
    {
        _enemyManager = new EnemyManager(enemyPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            _enemyManager.SpawnEnemy(path);
    }
}
