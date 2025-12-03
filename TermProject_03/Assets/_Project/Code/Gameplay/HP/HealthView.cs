using _Project.Code.Core.MVC;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : BaseView<float>
{
    [SerializeField] private Slider healthBar;


    public override void Initialize()
    {
        base.Initialize();

        if (healthBar != null)
        {
            healthBar.interactable = false;
            healthBar.value = 1.0f;
        }
    }

    public override void UpdateDisplay(float data)
    {
        if (healthBar != null)
            healthBar.value = data / 100.0f;
    }
}