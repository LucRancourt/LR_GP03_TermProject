using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Scriptable Objects/Wave")]
public class WaveConfig : ScriptableObject
{
    [field: SerializeField] public EnemyData[] EnemyData;
}

[System.Serializable]
public class EnemyData
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private int count;
    [SerializeField] private float spawnDelay;
}