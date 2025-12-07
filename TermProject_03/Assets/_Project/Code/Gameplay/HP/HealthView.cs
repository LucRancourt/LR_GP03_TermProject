using _Project.Code.Core.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : BaseView<float>
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;


    public override void Initialize()
    {
        base.Initialize();

        if (healthBar != null)
        {
            healthBar.interactable = false;
            healthBar.value = 1.0f;
        }

        if (healthText != null)
        {
            healthText.text = "#/0";
        }
    }

    public override void UpdateDisplay(float currentHealth, float maxHealth)
    {
        if (healthBar != null)
            healthBar.value = currentHealth / maxHealth;

        if (healthText != null)
            healthText.text = currentHealth + "/" + maxHealth;
    }
}