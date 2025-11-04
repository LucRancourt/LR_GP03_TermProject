using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private TowerData[] towers;

    public event Action<TowerData> OnTowerSelected;

    private void Awake()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);

        int index = 0;

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnPressWithParameters(index));
            index++;
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
