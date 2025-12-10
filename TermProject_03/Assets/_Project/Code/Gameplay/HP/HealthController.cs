using System;
using UnityEngine;

using _Project.Code.Core.MVC;


public class HealthController : BaseController<HealthModel, HealthView>
{
    public HealthController(HealthModel model, HealthView view) : base(model, view) { }

    private float _maxHealth = 0.0f;
    public float CurrentHealth { get; private set; }

    private bool _displayUI = false;

    public event Action OnDiedEvent;


    public void SetMaxHealth(float value)
    {
        if (value == 0.0f)
            Debug.LogError("MaxHealth cannot be set to 0!");

        _maxHealth = value;
        Model?.SetMaxHealth(_maxHealth);
        CurrentHealth = Model.Data;

        View?.UpdateDisplay(CurrentHealth, _maxHealth);
    }

    public override void Initialize()
    {
        Model?.Initialize();
        View?.Initialize();
    }

    public void SetUIDisplay(bool state)
    {
        _displayUI = state;

        if (_displayUI)
            View?.Show();
        else
            View?.Hide();
    }

    public override void Enable()
    {
        if (_isEnabled) return;

        _isEnabled = true;
        OnEnabled();
    }

    protected override void OnModelDataChanged()
    {
        if (_isEnabled)
        {
            if (Model != null)
                CurrentHealth = Model.Data;

            View?.UpdateDisplay(CurrentHealth, _maxHealth);
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
