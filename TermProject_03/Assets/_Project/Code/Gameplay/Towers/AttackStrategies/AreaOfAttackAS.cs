using UnityEngine;

[CreateAssetMenu(fileName = "NewAreaOfAttackAS", menuName = "Scriptable Objects/Attack Strategies/AoE Attack")]
public class AreaOfAttackAS : BaseAttackStrategy
{
    //public OutwardBurstParticle ParticlePrefab;

    public override void Execute(AttackInput attackInput)
    {
        //if (ParticlePrefab)
        foreach(BaseDamageable enemy in attackInput.Targets)
        {
            enemy.Damage(Damage);
        }
    }
}
