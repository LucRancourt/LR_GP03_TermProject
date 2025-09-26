using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class Wave : ScriptableObject
{
    [field: SerializeField] public AudioClip Clip { get; private set; }
}

[System.Serializable]
public class Info
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float spawnDelay;
}