using UnityEngine;

[CreateAssetMenu(fileName = "SpawnedUnitConfig", menuName = "Scriptable Objects/SpawnedUnit")]
public class SpawnedUnitConfig : ScriptableObject
{
    public GameObject Model;
    public float Health;
    public float Speed;
}
