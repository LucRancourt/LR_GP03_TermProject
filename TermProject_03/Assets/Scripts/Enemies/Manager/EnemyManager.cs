using UnityEngine;

public class EnemyManager
{
    // Constructor
    public EnemyManager(GameObject enemyPrefab, int initialAmount = 30)
    {
        FillPool(enemyPrefab, initialAmount);
    }


    // Variables
    private Pool _enemyPool;


    // Functions
    private void FillPool(GameObject enemyPrefab, int initialAmount = 30)
    {
        if (enemyPrefab.GetComponent<Enemy>())
            _enemyPool = new Pool(initialAmount, enemyPrefab);
        else
            Debug.LogError("Invalid EnemyPrefab to Pool!");
    }

    public void SpawnEnemy(NavPath path)
    {
        if (_enemyPool.PullFromPool().TryGetComponent(out Enemy enemySpawned))
            enemySpawned.Initialize(this, path);
    }

    public void DespawnEnemy(Enemy enemyToDespawn)
    {
        _enemyPool.ReturnToPool(enemyToDespawn.gameObject);
    }
}
