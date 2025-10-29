using UnityEngine;

using _Project.Code.Core.Factory;


public class EnemyManager
{
    private PooledFactory<Enemy> _enemyPoolFactory;


    public EnemyManager(Enemy enemyPrefab)
    {
        if (enemyPrefab.GetComponent<Enemy>())
            _enemyPoolFactory = new PooledFactory<Enemy>(enemyPrefab);
        else
            Debug.LogError("Invalid EnemyPrefab to Pool!");
    }


    public void SpawnEnemy(NavPath path)
    {
        Enemy enemy = _enemyPoolFactory.Create();
        enemy.Initialize(path);
        enemy.OnDiedEvent += DespawnEnemy;
    }

    public void DespawnEnemy(Enemy enemyToDespawn)
    {
        enemyToDespawn.OnDiedEvent -= DespawnEnemy;
        _enemyPoolFactory.Return(enemyToDespawn);
    }
}
