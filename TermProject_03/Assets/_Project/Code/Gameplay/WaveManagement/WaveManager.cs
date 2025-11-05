using System;
using System.Collections;
using UnityEngine;

using _Project.Code.Core.General;


public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveSet waveSet;
    [SerializeField] private NavPath path;

    private int _currentWaveIndex = 0;
    private EnemyManager _enemyManager;

    public event Action WaveCompleted;
    public event Action AllWavesCompleted;

    private Coroutine _waveCompletedCoroutine;
    private int _wavesToSpawn;
    private int _wavesCompleted = 0;


    public void Awake()
    {
        _enemyManager = new EnemyManager(waveSet);
    }

    public void StartNextWave()
    {
        if (_currentWaveIndex >= waveSet.waves.Length)
            return;


        _wavesToSpawn += waveSet.waves[_currentWaveIndex].enemiesToSpawn.Length;

        foreach (EnemyGroup enemyGroup in waveSet.waves[_currentWaveIndex].enemiesToSpawn)
        {
            StartCoroutine(SpawnWave(enemyGroup));
        }


        _currentWaveIndex++;

        if (_currentWaveIndex >= waveSet.waves.Length)
            AllWavesCompleted?.Invoke();
    }

    private IEnumerator SpawnWave(EnemyGroup enemyGroup)
    {
        yield return new WaitForSeconds(enemyGroup.StartDelay.RandomValue());

        for (int i = 0; i < enemyGroup.Count; i++)
        {
            _enemyManager.SpawnEnemy(enemyGroup.Enemy, path);

            if (i != enemyGroup.Count - 1)
                yield return new WaitForSeconds(enemyGroup.DelayBetweenEnemies.RandomValue());
        }

        SpawningComplete();
    }

    private void SpawningComplete()
    {
        _wavesCompleted++;

        if (_wavesCompleted != _wavesToSpawn) return;

        StartEndWaveCheck();
    }

    private void StartEndWaveCheck()
    {
        _waveCompletedCoroutine = CoroutineExecutor.Instance.CallbackOnConditionMet(
            () => _enemyManager.ActiveEnemyCount == 0,
            WaveCompleted);
    }

    public void CancelEndWaveCheck()
    {
        if (_waveCompletedCoroutine != null)
            CoroutineExecutor.Instance.CancelCoroutine(_waveCompletedCoroutine);
    }
}