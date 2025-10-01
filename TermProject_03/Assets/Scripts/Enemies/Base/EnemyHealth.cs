using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    // Variables
    private float _maxHealth;
    private float _currentHealth;

    public event Action OnDiedEvent;


    // Functions
    private void Start()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        collider.isTrigger = true;
        collider.includeLayers = LayerMask.NameToLayer("BaseLayer");
        if (collider.includeLayers == -1)
            Debug.LogError("BaseLayer can not be found!");
    }

    public void SetHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void OnTakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0.0f)
            OnDied();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Base>())
            OnDied(true);
    }

    private void OnDied(bool dealBaseDamage = false)
    {
        if (dealBaseDamage)
        {
            Base.Instance.OnTakeDamage(_currentHealth);
        }

        OnDiedEvent?.Invoke();
    }
}
