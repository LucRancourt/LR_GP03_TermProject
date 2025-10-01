using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Base : Singleton<Base>, IDamageable
{
    // Variables
    private float _maxHealth;
    private float _currentHealth;

    public event Action OnGameOverEvent;


    // Functions
    protected override void Awake()
    {
        base.Awake();

        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void Initialize(float maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public void OnTakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0.0f)
            OnGameOverEvent?.Invoke();
    }
}
