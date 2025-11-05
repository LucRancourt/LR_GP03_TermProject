using _Project.Code.Core.General;
using _Project.Code.Core.MVC;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUIWindow : Singleton<TowerUIWindow>, IView<TowerData>
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

    public void UpdateDisplay(TowerData data)
    {
        if (data == null) return;

        nameText.text = data.Name;
        unitLimitText.text = data.UnitLimit.ToString();
        iconImage.sprite = data.Icon;

        SetOptionalRow(specialEffectRow, testText, "Poison ", data.SpaceTaken.ToString());
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