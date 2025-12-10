using _Project.Code.Core.General;
using System;
using System.Collections;
using UnityEngine;

public class Bullet : BaseProjectile
{
    [SerializeField] private float speed;

    private Enemy _target;
    private float _damage;

    //public override event Action OnTargetReached;


    public override void Initialize(Vector3 startPos, Enemy target, float damage)
    {
        transform.position = startPos;

        _target = target;
        _damage = damage;

        StartCoroutine(MoveTowardsTarget(_target.transform.position, speed));
    }

    public IEnumerator MoveTowardsTarget(Vector3 targetPosition, float speed, float stopDistance = 0.15f)
    {
        Vector3 direction = MyUtils.GetDirection(targetPosition, transform.position);
        transform.rotation = Quaternion.LookRotation(direction);

        while (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }

        if (_target)
        {
            _target.Damage(_damage);
        }

        Destroy(gameObject);
        //OnTargetReached?.Invoke();
    }
}
