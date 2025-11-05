using _Project.Code.Core.Strategy;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackStrategy", menuName = "Scriptable Objects/Attack Strategies/Base")]
public class AttackStrategy : ScriptableStrategy<AttackInput>
{
    [SerializeField] protected GameObject attackObject;

    public event Action OnDamageTarget;

    protected GameObject projectile;
    private PathNavigator pathNav;

    public override void Execute(AttackInput attackInput)
    {
        projectile = Instantiate(attackObject);
        projectile.transform.position = attackInput.AttackOrigin.position;
        projectile.transform.rotation = attackInput.AttackOrigin.rotation;

        pathNav = projectile.AddComponent<PathNavigator>();
        pathNav.TEMP_HitTarget += OnHitTarget;
        pathNav.TEMP_ChaseTarget(attackInput.Target.position, 10.0f);
    }

    private void OnHitTarget()
    {
        pathNav.TEMP_HitTarget -= OnHitTarget;

        OnDamageTarget?.Invoke();

        if (projectile == null) return;
        projectile.SetActive(false);
        Destroy(projectile);
    }
}

public class AttackInput
{
    public Transform AttackOrigin;
    public Transform Target;

    public AttackInput(Transform attackOrigin, Transform targetOrigin)
    {
        AttackOrigin = attackOrigin;
        Target = targetOrigin;
    }
}