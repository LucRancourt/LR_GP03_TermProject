using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelInfo", menuName = "Scriptable Objects/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    [SerializeField, Range(1, 10)] public int Difficulty;
    public string Description;
}
