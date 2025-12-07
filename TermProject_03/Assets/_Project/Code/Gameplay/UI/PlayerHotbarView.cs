using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHotbarView : MonoBehaviour
{
    private Button[] _buttons;
    [SerializeField] private StatRow[] inventorySpaces;

    public event Action<int> OnTowerClicked;


    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>(true);

        for (int i = 0; i < _buttons.Length; i++)
        {
            int index = i;
            _buttons[i].onClick.AddListener(() => OnTowerClicked?.Invoke(index));
        }
    }

    public void UpdateDisplay(BaseTowerData[] towerDatas)
    {
        for (int i = 0; i < (towerDatas.Length > inventorySpaces.Length ? inventorySpaces.Length : towerDatas.Length); i++)
        {
            inventorySpaces[i].SetStatRow(towerDatas[i].GetDefaultIcon(), towerDatas[i].GetPlacementCost().ToString());

            if (!PlayerWallet.Instance.SufficientFunds(towerDatas[i].GetPlacementCost()))
                _buttons[i].interactable = false;
            else
                _buttons[i].interactable = true;
        }
    }
}
