using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using _Project.Code.Core.General;


public class TowerUIWindow : Singleton<TowerUIWindow>
{
    [SerializeField] private CanvasGroup panel;

    [Header("Constants")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI unitLimitText;
    [SerializeField] private Image iconImage;

    [Header("Optionals")]
    [SerializeField] private GameObject specialEffectRow;
    [SerializeField] private TextMeshProUGUI testText;

    [Header("Buttons")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeCost;
    [SerializeField] private Button sellButton;
    [SerializeField] private TextMeshProUGUI sellValue;

    public event Action OnUpgrade;
    public event Action OnSell;


    protected override void Awake()
    {
        base.Awake();

        Hide();
        ClearButtonListeners();

        upgradeButton.onClick.AddListener(() => OnUpgrade?.Invoke());
        sellButton.onClick.AddListener(() => OnSell?.Invoke());
    }

    public void UpdateDisplay(Tower tower, int currentUnitLimit)
    {
        if (tower == null) return;

        BaseTowerData towerData = tower.TowerData;

        nameText.text = towerData.Name;
        unitLimitText.text = currentUnitLimit.ToString() + " / " + towerData.UnitLimit.ToString();
        iconImage.sprite = towerData.Icon;

        TierTowerData tierTowerData = towerData.GetTowerTierData(tower.TowerTier);

        upgradeCost.text = "Upgrade<br><color=#ff0000><b>" + tierTowerData.Cost.ToString() + "</b><color=#000000>";
        sellValue.text = "Sell<br><color=#00ff00><b>" + tierTowerData.SellValue.ToString() + "</b><color=#000000>";

        SetOptionalRow(specialEffectRow, testText, "Poison ", tower.TowerData.GetTowerTierData(tower.TowerTier).Cost.ToString());
        SetOptionalRow(specialEffectRow, testText, "wwww ", tower.TowerData.GetTowerTierData(tower.TowerTier).Cost.ToString());
        SetOptionalRow(specialEffectRow, testText, "Poisaaaaon ", tower.TowerData.GetTowerTierData(tower.TowerTier).Cost.ToString());
        SetOptionalRow(specialEffectRow, testText, "Poisssssson ", tower.TowerData.GetTowerTierData(tower.TowerTier).Cost.ToString());
        SetOptionalRow(specialEffectRow, testText, "Poisoxxxxxxn ", tower.TowerData.GetTowerTierData(tower.TowerTier).Cost.ToString());
    }

    private void SetOptionalRow(GameObject row, TextMeshProUGUI text, string title, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            row.SetActive(true);
            text.text = value;
        }
        else
        {
            row.SetActive(false);
        }
    }

    public void Dispose()
    {
        ClearButtonListeners();
    }

    private void ClearButtonListeners()
    {
        upgradeButton.onClick.RemoveAllListeners();
        sellButton.onClick.RemoveAllListeners();
    }

    public void Initialize() { Hide(); }
    public void UpdateDisplay() { Debug.LogWarning("Should not be calling on this!"); }

    public void Show()
    {
        panel.alpha = 1.0f;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }

    public void Hide()
    {
        panel.blocksRaycasts = false;
        panel.interactable = false;
        panel.alpha = 0.0f;
    }
}