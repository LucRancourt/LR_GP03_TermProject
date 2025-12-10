using UnityEngine;


[CreateAssetMenu(fileName = "NewSingleShotAS", menuName = "Scriptable Objects/Attack Strategies/Single Shot")]
public class SingleShotAS : BaseAttackStrategy
{
    public Bullet BulletPrefab;

    public override void Execute(AttackInput attackInput)
    {
        base.Execute(attackInput);

        if (BulletPrefab)
        {
            Instantiate(BulletPrefab).Initialize(attackInput.AttackOrigin.position, attackInput.Targets[0], Damage);
        }
    }
}
