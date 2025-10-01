using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(HealthSystem))]

public class Base : Singleton<Base>
{
    // Variables
    private HealthSystem _healthSystem;


    // Functions
    protected override void Awake()
    {
        base.Awake();

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;

        GetComponent<BoxCollider>().isTrigger = true;

        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDiedEvent += OnGameOver;
    }

    public void Initialize(float maxHealth)
    {
        _healthSystem.SetMaxHealth(maxHealth);
    }

    private void OnGameOver()
    {
        GameManager.Instance.SetGameOver();
    }
}
