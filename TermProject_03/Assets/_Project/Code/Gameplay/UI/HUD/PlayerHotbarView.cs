using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHotbarView : MonoBehaviour
{
    private Button[] _buttons;
    [SerializeField] private StatRow[] inventorySpaces;

    private bool[] _bHitLimit;

    public event Action<int> OnTowerClicked;


    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>(true);
        _bHitLimit = new bool[_buttons.Length];

        for (int i = 0; i < _buttons.Length; i++)
        {
            int index = i;
            _buttons[i].onClick.AddListener(() => OnTowerClicked?.Invoke(index));
        }
    }

    public void UpdateDisplay(BaseTowerData[] towerDatas, bool bResetCurrentUnitLimit = false)
    {
        int amountOfValidTowers = towerDatas.Length > inventorySpaces.Length ? inventorySpaces.Length : towerDatas.Length;
        int amountOfInvalidTowers = inventorySpaces.Length - amountOfValidTowers;

        for (int i = 0; i < amountOfValidTowers; i++)
        {
            if (bResetCurrentUnitLimit)
            {
                inventorySpaces[i].SetStatRow(towerDatas[i].GetDefaultIcon(),
                                                         towerDatas[i].GetPlacementCost().ToString(),
                                                         "0/" + towerDatas[i].UnitLimit);
            }
            else
            {
                inventorySpaces[i].SetStatRow(towerDatas[i].GetDefaultIcon(),
                                                         towerDatas[i].GetPlacementCost().ToString());
            }
           

            if (!PlayerWallet.Instance.SufficientFunds(towerDatas[i].GetPlacementCost()) || _bHitLimit[i])
                _buttons[i].interactable = false;
            else
                _buttons[i].interactable = true;
        }

        for (int i = 0; i < amountOfInvalidTowers; i++)
        {
            inventorySpaces[inventorySpaces.Length - 1 - i].SetStatRow(null, "", "");
            _buttons[inventorySpaces.Length - 1 - i].interactable = false;
        }
    }

    public void UpdateSingleDisplay(BaseTowerData towerData, int currentUnitLimit, int index)
    {
        inventorySpaces[index].SetStatRow(towerData.GetDefaultIcon(),
                                      towerData.GetPlacementCost().ToString(),
                                      currentUnitLimit + "/" + towerData.UnitLimit);

        if (currentUnitLimit >= towerData.UnitLimit)
        {
            _bHitLimit[index] = true;
            _buttons[index].interactable = false;
        }
        else
        {
            _bHitLimit[index] = false;

            if (PlayerWallet.Instance.SufficientFunds(towerData.GetPlacementCost()))
                _buttons[index].interactable = true;
        }
    }
}
