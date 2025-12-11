using _Project.Code.Core.General;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBurstShotAS", menuName = "Scriptable Objects/Attack Strategies/Burst Shot")]
public class BurstShotAS : BaseAttackStrategy
{
    public Bullet BulletPrefab;
    public int BurstCount = 3;
    public float BurstDelay = 0.2f;


    public override void Execute(AttackInput attackInput)
    {
        CoroutineExecutor.Instance.StartCoroutineExec(FireBurst(attackInput));
    }

    private IEnumerator FireBurst(AttackInput attackInput)
    {
        for (int i = 0; i < BurstCount; i++)
        {
            if (attackInput.Targets.Count == 0)
                break;

            if (BulletPrefab)
            {
                base.Execute(attackInput);
                Instantiate(BulletPrefab).Initialize(attackInput.AttackOrigin.position, attackInput.Targets[0], Damage);
            }

            yield return new WaitForSeconds(BurstDelay);
        }
    }
}