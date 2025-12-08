using _Project.Code.Core.MVC;
using TMPro;
using UnityEngine;

public class WaveCounter : BaseView<int>
{
    [SerializeField] private TextMeshProUGUI waveCountText;


    public override void UpdateDisplay(int currentWave, int totalWaves)
    {
        waveCountText.text = "Wave: " + currentWave + "/" + totalWaves;
    }
}
