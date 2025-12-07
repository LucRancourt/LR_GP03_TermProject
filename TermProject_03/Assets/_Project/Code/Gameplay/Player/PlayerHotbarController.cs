using System;
using UnityEngine;

public class PlayerHotbarController : MonoBehaviour
{
    [SerializeField] private BaseTowerData[] towers;
    [SerializeField] private PlayerHotbarView playerInventoryHUD;

    public event Action<BaseTowerData> OnTowerSelected;


    private void Start()
    {
        playerInventoryHUD.UpdateDisplay(towers, true);
        PlayerWallet.Instance.OnDataChanged += (int amount) => playerInventoryHUD.UpdateDisplay(towers);

        playerInventoryHUD.OnTowerClicked += OnPressWithParameters;
    }

    private void OnPressWithParameters(int index)
    {
        if (index >= towers.Length) return;

        OnTowerSelected?.Invoke(towers[index]);
    }

    public void UpdateView(BaseTowerData towerData, int currentUnitLimit) 
    { 
        playerInventoryHUD.UpdateSingleDisplay(towerData, currentUnitLimit, Array.IndexOf(towers, towerData)); 
    }

    public BaseTowerData[] GetTowerList() { return towers; }

    public BaseTowerData GetTowerData(int index)
    {
        index--;

        if (index >= towers.Length) return null;

        return towers[index];
    }
}
