using System;

using _Project.Code.Core.General;
using _Project.Code.Core.MVC;


public class HealthModel : IModel<float>
{
    public event Action OnDataChanged;
    public event Action OnDied;

    private float _currentHealth;
    private float _maxHealth;

    public float Data => _currentHealth;



    public void Initialize()
    {
    }

    public void SetMaxHealth(float value)
    {
        _maxHealth = value;
        _currentHealth = _maxHealth;

        OnDataChanged?.Invoke();
    }

    public void OnDamaged(float value)
    {
        _currentHealth = MyUtils.Clamp(_currentHealth - (MyUtils.Abs(value)), 0.0f, _maxHealth);

        OnDataChanged?.Invoke();

        if (_currentHealth == 0.0f)
            OnDied?.Invoke();
    }

    public void OnHealed(float value)
    {
        _currentHealth = MyUtils.Clamp(_currentHealth + (MyUtils.Abs(value)), 0.0f, _maxHealth);

        OnDataChanged?.Invoke();
    }

    public virtual void Dispose()
    {
        OnDataChanged = null;
        OnDied = null;
    }
}
