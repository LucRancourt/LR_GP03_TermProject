using UnityEngine;

[CreateAssetMenu(fileName = "BaseEnemyConfig", menuName = "Scriptable Objects/BaseEnemyConfig")]
public class BaseEnemyConfig : ScriptableObject
{
    [field: SerializeField] public string Name;
    [field: SerializeField] public GameObject Prefab;
    [field: SerializeField] public float Health;
    [field: SerializeField] public float Speed;
}
