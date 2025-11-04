using System.Collections.Generic;

using _Project.Code.Core.Factory;
using UnityEngine;

public class EnemyManager
{
    private Dictionary<string, PooledFactory<Enemy>> _enemyPoolFactory = new();
    public int ActiveEnemyCount { get; private set; } = 0;

    public EnemyManager(WaveSet waveSet)
    {
        Dictionary<EnemyData, int> enemyCounts = new();

        foreach (WaveData wave in waveSet.waves)
        {
            foreach (EnemyGroup enemy in wave.enemiesToSpawn)
            {
                if (enemyCounts.TryGetValue(enemy.Enemy, out int count))
                {
                    if (enemy.Count > count)
                        enemyCounts[enemy.Enemy] = enemy.Count;
                }
                else
                {
                    enemyCounts[enemy.Enemy] = enemy.Count;
                }
            }
        }

        foreach (EnemyData enemyToPool in enemyCounts.Keys)
        {
            _enemyPoolFactory[enemyToPool.Name] = new PooledFactory<Enemy>(enemyToPool.Prefab, enemyCounts[enemyToPool]);
        }
    }

    public void SpawnEnemy(EnemyData enemyData, NavPath path)
    {
        Enemy enemy = _enemyPoolFactory[enemyData.Name].Create();
        enemy.Initialize(enemyData, path);
        enemy.OnEnemyDiedEvent += DespawnEnemy;

        ActiveEnemyCount++;
    }

    public void DespawnEnemy(Enemy enemyToDespawn)
    {
        enemyToDespawn.OnEnemyDiedEvent -= DespawnEnemy;
        _enemyPoolFactory[enemyToDespawn.Name].Return(enemyToDespawn);

        ActiveEnemyCount--;
    }
}
