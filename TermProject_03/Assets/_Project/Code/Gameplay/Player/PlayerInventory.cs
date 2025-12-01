using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private BaseTowerData[] towers;
    [SerializeField] private GameObject towerInventoryHUD;

    public event Action<BaseTowerData> OnTowerSelected;

    private void Awake()
    {
        Button[] buttons = towerInventoryHUD.GetComponentsInChildren<Button>(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnPressWithParameters(index));
        }
    }

    private void OnPressWithParameters(int index)
    {
        if (index >= towers.Length) return;

        OnTowerSelected?.Invoke(towers[index]);
    }

    public BaseTowerData[] GetTowerList()
    {
        return towers;
    }

    public BaseTowerData GetTowerData(int index)
    {
        index--;

        if (index >= towers.Length) return null;

        return towers[index];
    }
}
