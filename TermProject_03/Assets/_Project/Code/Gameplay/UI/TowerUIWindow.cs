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
    [SerializeField] private Button sellButton;

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

    public void UpdateDisplay(BaseTowerData towerData, int currentUnitLimit)
    {
        if (towerData == null) return;

        nameText.text = towerData.Name;
        unitLimitText.text = currentUnitLimit.ToString() + " / " + towerData.UnitLimit.ToString();
        iconImage.sprite = towerData.Icon;

        SetOptionalRow(specialEffectRow, testText, "Poison ", towerData.SpaceTaken.ToString());
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