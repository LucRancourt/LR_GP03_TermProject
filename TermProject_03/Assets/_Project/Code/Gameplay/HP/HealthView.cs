using _Project.Code.Core.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : BaseView<float>
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private bool bDynamicColor = true;

    private Color _red = new Color(227.0f / 255.0f, 61.0f / 255.0f, 65.0f / 255.0f);//new Color(254.0f / 255.0f, 161.0f / 255.0f, 163.0f / 255.0f);
    private Color _yellow = new Color(235.0f / 237.0f, 227.0f / 255.0f, 0.0f / 255.0f);//new Color(254.0f / 255.0f, 251.0f / 255.0f, 161.0f / 255.0f);
    private Color _green = new Color(69.0f / 255.0f, 227.0f / 255.0f, 61.0f / 255.0f);//new Color(161.0f / 255.0f, 254.0f / 255.0f, 167.0f / 255.0f);


    protected override void Awake() { }

    public override void Initialize()
    {
        base.Initialize();

        if (healthBar != null && healthBar.type == Image.Type.Filled)
        {
            healthBar.fillAmount = 1.0f;

            if (bDynamicColor)
                UpdateColor(1.0f);
        }

        if (healthText != null)
            healthText.text = "#/0";
    }

    public override void UpdateDisplay(float currentHealth, float maxHealth)
    {
        if (healthBar != null && healthBar.type == Image.Type.Filled)
        {
            float currentPercentage = currentHealth / maxHealth;
            healthBar.fillAmount = currentPercentage;

            if (bDynamicColor)
                UpdateColor(currentPercentage);
        }

        if (healthText != null)
            healthText.text = currentHealth + "/" + maxHealth;
    }

    private void UpdateColor(float currentPercentage)
    {
        if (currentPercentage <= 0.33f)
            healthBar.color = _red;
        else if (currentPercentage <= 0.66f)
            healthBar.color = _yellow;
        else
            healthBar.color = _green;
    }
}