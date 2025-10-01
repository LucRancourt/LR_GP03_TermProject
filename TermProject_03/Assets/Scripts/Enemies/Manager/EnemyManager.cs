using UnityEngine;

public class EnemyManager
{
    // Constructor
    public EnemyManager(GameObject enemyPrefab)
    {
        FillPool(enemyPrefab);
    }


    // Variables
    private Pool _enemyPool;


    // Functions
    private void FillPool(GameObject enemyPrefab)
    {
        if (enemyPrefab.GetComponent<Enemy>())
            _enemyPool = new Pool(enemyPrefab);
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
