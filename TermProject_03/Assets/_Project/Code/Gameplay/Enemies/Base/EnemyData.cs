using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Scriptable Objects/Enemy/Data")]
public class EnemyData : ScriptableObject
{
    public string Name;
    public Enemy Prefab;
    public float Health;
    public float Speed;
    public int Value;
}