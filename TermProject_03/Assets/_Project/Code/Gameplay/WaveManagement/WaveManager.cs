using System;
using System.Collections;
using UnityEngine;

using _Project.Code.Core.General;


public class WaveManager : MonoBehaviour
{
    private bool bWasInitialized = false;

    [Tooltip("One for each of the 3 Difficulty levels")]
    [SerializeField] private WaveSet[] waveSets = new WaveSet[3];
    private WaveSet _activeWaveSet;

    private int _selectedDifficulty;

    [SerializeField] private NavPath[] paths;

    private EnemyManager[] _enemyManagers = new EnemyManager[3];
    private EnemyManager _activeEnemyManager;

    public event Action OnWaveCompleted;

    public int CurrentWaveIndex { get; private set; } = 0;
    private Coroutine _waveCompletedCoroutine;
    private int _wavesToSpawn;
    private int _wavesCompleted = 0;


    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            _enemyManagers[i] = new EnemyManager(waveSets[i]);
        }        
    }

    public void Initialize()
    {
        _selectedDifficulty = (int)LevelDifficulty.Instance.DifficultyLevel;

        _activeWaveSet = waveSets[_selectedDifficulty];
        _activeEnemyManager = _enemyManagers[_selectedDifficulty];

        bWasInitialized = true;
    }

    public bool AreAllWavesSpawned() { return CurrentWaveIndex >= _activeWaveSet.waves.Length; }
    public int TotalNumberOfWaves() { return _activeWaveSet != null ? _activeWaveSet.waves.Length : -1; }

    public void StartNextWave()
    {
        if (!bWasInitialized) return;

        if (AreAllWavesSpawned())
            return;


        _wavesToSpawn += _activeWaveSet.waves[CurrentWaveIndex].enemiesToSpawn.Length;

        foreach (EnemyGroup enemyGroup in _activeWaveSet.waves[CurrentWaveIndex].enemiesToSpawn)
        {
            StartCoroutine(SpawnWave(enemyGroup));
        }


        CurrentWaveIndex++;
    }

    private IEnumerator SpawnWave(EnemyGroup enemyGroup)
    {
        yield return new WaitForSeconds(enemyGroup.StartDelay.RandomValue());

        for (int i = 0; i < enemyGroup.Count; i++)
        {
            _activeEnemyManager.SpawnEnemy(enemyGroup.Enemy, MyUtils.RandomFrom(paths));

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
            () => _activeEnemyManager.ActiveEnemyCount == 0,
            OnWaveCompleted);
    }

    public void CancelEndWaveCheck()
    {
        if (_waveCompletedCoroutine != null)
            CoroutineExecutor.Instance.CancelCoroutine(_waveCompletedCoroutine);
    }
}