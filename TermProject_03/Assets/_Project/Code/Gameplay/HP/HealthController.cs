using System;
using UnityEngine;

using _Project.Code.Core.MVC;


public class HealthController : BaseController<HealthModel, HealthView>, IDamageable
{
    public HealthController(HealthModel model, HealthView view) : base(model, view) { }

    private float _maxHealth = 0.0f;
    public float CurrentHealth { get; private set; }

    public event Action OnDiedEvent;


    public void SetMaxHealth(float value)
    {
        if (value == 0.0f)
            Debug.LogError("MaxHealth cannot be set to 0!");

        _maxHealth = value;
        Model?.SetMaxHealth(_maxHealth);
        CurrentHealth = Model.Data;
    }

    public override void Initialize()
    {
        Model?.Initialize();
        View?.Initialize();
    }
    protected override void OnModelDataChanged()
    {
        if (_isEnabled)
        {
            if (Model != null)
                CurrentHealth = Model.Data;

            View?.UpdateDisplay(CurrentHealth);
        }
    }

    protected override void OnEnabled()
    {
        if (Model != null)
        {
            Model.SetMaxHealth(_maxHealth);
            Model.OnDataChanged += OnModelDataChanged;
            Model.OnDied += OnDied;
        }
    }

    protected override void OnDisabled()
    {
        if (Model != null)
        {
            Model.OnDataChanged -= OnModelDataChanged;
            Model.OnDied -= OnDied;
        }
    }

    public override void Dispose()
    {
        if (Model != null)
        {
            Model.OnDataChanged -= OnModelDataChanged;
            Model.OnDied -= OnDied;
            Model.Dispose();
        }

        View?.Dispose();
    }

    private void OnDied()
    {
        OnDiedEvent?.Invoke();
    }

    public void OnDamaged(float value)
    {
        if (_isEnabled)
            Model?.OnDamaged(value);
    }

    public void OnHealed(float value)
    {
        if (_isEnabled)
            Model?.OnHealed(value);
    }
}
