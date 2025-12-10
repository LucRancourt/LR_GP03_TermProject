using System;
using UnityEngine;

[RequireComponent(typeof(HealthView))]

public abstract class BaseDamageable : MonoBehaviour, IDamageable
{
    private HealthController _healthController;

    public event Action OnDied;


    protected void SetupHealthSystem()
    {
        HealthView healthView = GetComponent<HealthView>();
        HealthModel healthModel = new HealthModel();

        _healthController = new HealthController(healthModel, healthView);
        _healthController.Initialize();
    }

    protected void SetHealthDefaults(float maxHealth)
    {
        _healthController.SetMaxHealth(maxHealth);
        _healthController.OnDiedEvent += OnUnitDied;

        _healthController.Enable();
    }

    protected void ResetHealthDefaults()
    {
        _healthController.OnDiedEvent -= OnUnitDied;

        _healthController.Disable();
    }

    private void OnUnitDied()
    {
        OnDied?.Invoke();
    }

    protected float GetCurrentHealth()
    {
        return _healthController.CurrentHealth;
    }

    protected void SetUIDisplay(bool state)
    {
        _healthController.SetUIDisplay(state);
    }

    public void Damage(float value) => _healthController.OnDamaged(value);
    public void Heal(float value) => _healthController.OnHealed(value);
}