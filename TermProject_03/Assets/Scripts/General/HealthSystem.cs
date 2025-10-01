using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    // Variables
    private float _maxHealth;
    public float CurrentHealth { get; private set; }

    public event Action OnDiedEvent;


    // Functions
    public void SetMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
        CurrentHealth = _maxHealth;
    }

    public void OnDamaged(float value)
    {
        CurrentHealth -= MyUtils.Abs(value);

        UpdateHealth();
    }

    public void OnHealed(float value)
    {
        CurrentHealth += MyUtils.Abs(value);

        UpdateHealth();
    }

    private void UpdateHealth()
    {
        CurrentHealth = MyUtils.Clamp(CurrentHealth, 0.0f, _maxHealth);

        UpdateSlider();

        if (CurrentHealth == 0.0f)
            OnDiedEvent?.Invoke();
    }

    private void UpdateSlider()
    {

    }
}
