using _Project.Code.Core.General;
using _Project.Code.Core.Strategy;
using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBeamAttackStrategy", menuName = "Scriptable Objects/Attack Strategies/Beam")]
public class BeamAttackStrategy : AttackStrategy
{
    public new event Action OnDamageTarget;

    private AttackInput _attackInput;
    Coroutine _routine;

    public override void Execute(AttackInput attackInput)
    {
        _attackInput = attackInput;

        projectile = Instantiate(attackObject);
        projectile.transform.position = attackInput.AttackOrigin.position;
        Vector3 direction = (attackInput.Target.position - projectile.transform.position).normalized;
        projectile.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

        Vector3 scale = projectile.transform.localScale;
        scale.y = 5.0f;
        projectile.transform.localScale = scale;

        CoroutineExecutor.Instance.StartCoroutine(FollowTarget());
    }

    private int _time;
    private IEnumerator FollowTarget()
    {
        _time = 10;
        while (true)
        {
            if (_time % 2 == 0)
                OnDamageTarget?.Invoke();

            if (_time <= 0.0f)
            {
                OnDead();
                yield break;
            }

            Vector3 direction = (_attackInput.Target.position - projectile.transform.position).normalized;
            projectile.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

            yield return null;
        }
    }

    private IEnumerator DecrementTime()
    {
        yield return new WaitForSeconds(1.0f);

        _time -= 1;
        CoroutineExecutor.Instance.StartCoroutine(DecrementTime());
    }

    public void OnDead()
    {
        CoroutineExecutor.Instance.StopAllCoroutines();
           Destroy(projectile);
    }
}
