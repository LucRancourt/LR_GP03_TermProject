using UnityEngine;

using _Project.Code.Core.General;


[CreateAssetMenu(fileName = "NewWaveData", menuName = "Scriptable Objects/Wave/Data")]
public class WaveData : ScriptableObject
{
    [Header("Enemy Spawns")]
    public EnemyGroup[] enemiesToSpawn;


    private void OnValidate()
    {
        if (enemiesToSpawn != null)
        {
            foreach (EnemyGroup enemyGroup in enemiesToSpawn)
            {
                if (enemyGroup.Count == 0)
                    enemyGroup.Count = 1;

                if (!enemyGroup.Initialized)
                {
                    enemyGroup.StartDelay = new FloatRange(1.0f, 5.0f);
                    enemyGroup.DelayBetweenEnemies = new FloatRange(1.0f, 5.0f);

                    enemyGroup.Initialized = true;
                }
            }
        }
    }
}


[System.Serializable]
public class EnemyGroup
{
    public EnemyData Enemy;
    public int Count = 1;

    public FloatRange StartDelay = new FloatRange(1.0f, 5.0f);
    public FloatRange DelayBetweenEnemies = new FloatRange(1.0f, 5.0f);

    [HideInInspector] public bool Initialized = false; // tracks if defaults have been applied
}