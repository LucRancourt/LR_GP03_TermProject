using UnityEngine;


[CreateAssetMenu(fileName = "NewWaveSet", menuName = "Scriptable Objects/Wave/Set")]
public class WaveSet : ScriptableObject
{
    public WaveData[] waves;

    //[Header("Modifiers")]
    //public IModifier[] modifiers;    <- For later


    private void OnValidate()
    {
        // Something in here to make sure no duplicate modifiers are chosen
    }
}