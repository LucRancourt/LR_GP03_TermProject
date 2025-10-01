using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public string Name;
    [field: SerializeField] public GameObject Prefab;
    [field: SerializeField] public float Health;
    [field: SerializeField] public float Speed;
}
