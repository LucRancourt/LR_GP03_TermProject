using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHotbarView : MonoBehaviour
{
    [SerializeField] private StatRow[] inventorySpaces;

    public event Action<int> OnTowerClicked;


    private void Awake()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnTowerClicked?.Invoke(index));
        }
    }

    public void SetDisplay(BaseTowerData[] towerDatas)
    {
        for (int i = 0; i < (towerDatas.Length > inventorySpaces.Length ? inventorySpaces.Length : towerDatas.Length); i++)
        {
            inventorySpaces[i].SetStatRow(towerDatas[i].GetDefaultIcon(), towerDatas[i].GetPlacementCost().ToString());
        }
    }
}
