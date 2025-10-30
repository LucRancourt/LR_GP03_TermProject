using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour, IDamageable
{
    // Variables
    private float _maxHealth;
    public float CurrentHealth { get; private set; }

    private Image _healthBar;

    public event Action OnDiedEvent;


    // Functions
    private void Awake()
    {
        //_healthBar = FindFirstObjectByType<Canvas>().gameObject.AddComponent<Image>();
    }

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

        UpdateHealthBar();

        if (CurrentHealth == 0.0f)
            OnDiedEvent?.Invoke();
    }

    private void UpdateHealthBar()
    {
        Debug.Log(gameObject.name + " has " + CurrentHealth + " health!");
        //_healthBar.fillAmount = (CurrentHealth / _maxHealth);
    }
}
