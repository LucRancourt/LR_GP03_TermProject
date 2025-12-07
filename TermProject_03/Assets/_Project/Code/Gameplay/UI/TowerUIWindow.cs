using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using _Project.Code.Core.General;

[RequireComponent(typeof(StatRowManager))]

public class TowerUIWindow : Singleton<TowerUIWindow>
{
    [SerializeField] private CanvasGroup panel;

    [Header("Constants")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI unitLimitText;
    [SerializeField] private Image iconImage;

    [Header("Buttons")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeCost;
    [SerializeField] private Button sellButton;
    [SerializeField] private TextMeshProUGUI sellValue;

    private StatRowManager _statRowManager;

    private Tween _tween;
    [Header("Tween")]
    [SerializeField] private float duration = 0.25f;

    public event Action OnUpgrade;
    public event Action OnSell;


    protected override void Awake()
    {
        base.Awake();

        Hide();
        ClearButtonListeners();

        sellButton.onClick.AddListener(() => OnSell?.Invoke());

        _statRowManager = GetComponent<StatRowManager>();
    }

    public void UpdateDisplay(Tower tower, int currentUnitLimit)
    {
        if (tower == null) return;

        BaseTowerData towerData = tower.TowerData;

        nameText.text = towerData.Name;
        unitLimitText.text = currentUnitLimit.ToString() + "/" + towerData.UnitLimit.ToString();
        Debug.Log(towerData.Icon.name);
        iconImage.sprite = towerData.Icon;

        TierTowerData tierTowerData = towerData.GetTowerTierData(tower.TowerTier);

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.interactable = false;

        if (towerData.TryGetTowerTierData(tower.TowerTier + 1, out TierTowerData nextTierTowerData))
        {
            upgradeButton.onClick.AddListener(() => OnUpgrade?.Invoke());

            if (PlayerWallet.Instance.SufficientFunds(nextTierTowerData.Cost))
                upgradeButton.interactable = true;

            upgradeCost.text = "Upgrade<br><b>" + nextTierTowerData.Cost.ToString() + "</b>";
        }
        else
        {
            upgradeCost.text = "MAX";
        }

        sellValue.text = "Sell<br><b>" + tierTowerData.SellValue.ToString() + "</b>";


        _statRowManager.SetupStats(tierTowerData);
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

        _tween?.Kill();
        _tween = panel.transform.DOScale(1.0f, duration);
        _tween.Play();
    }

    public void Hide()
    {
        _tween?.Kill();
        _tween = panel.transform.DOScale(0.0f, duration);
        _tween.onComplete += HidePanel;
        _tween.Play();
    }
    
    private void HidePanel()
    {
        panel.blocksRaycasts = false;
        panel.interactable = false;
        panel.alpha = 0.0f;
    }
}