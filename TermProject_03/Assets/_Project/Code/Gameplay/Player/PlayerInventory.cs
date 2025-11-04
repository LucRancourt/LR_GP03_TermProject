using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private TowerData[] towers;
    [SerializeField] private GameObject towerInventoryHUD;

    public event Action<TowerData> OnTowerSelected;

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

    public TowerData[] GetTowerList()
    {
        return towers;
    }

    public TowerData GetTowerData(int index)
    {
        index--;

        if (index >= towers.Length) return null;

        return towers[index];
    }
}
