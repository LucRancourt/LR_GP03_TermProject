using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveCountText;
    public int CurrentWave { get; private set; } = 0;


    public void ResetWaveCount()
    {
        CurrentWave = 0;
        SetWaveCountText();
    }

    public void IncrementWaveCount()
    {
        CurrentWave++;
        SetWaveCountText();
    }

    private void SetWaveCountText()
    {
        waveCountText.text = "Wave: " + CurrentWave;
    }
}
