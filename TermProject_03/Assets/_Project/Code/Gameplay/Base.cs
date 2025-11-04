using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class Base : BaseDamageable
{
    [SerializeField] private float maxHealth;

    private void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;

        GetComponent<BoxCollider>().isTrigger = true;

        SetupHealthSystem();
        SetHealthDefaults(maxHealth);
    }
}
