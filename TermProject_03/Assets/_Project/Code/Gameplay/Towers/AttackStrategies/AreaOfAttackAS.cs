using UnityEngine;

[CreateAssetMenu(fileName = "NewAreaOfAttackAS", menuName = "Scriptable Objects/Attack Strategies/AoE Attack")]
public class AreaOfAttackAS : BaseAttackStrategy
{
    public ParticleSystem ParticlePrefab;

    public override void Execute(AttackInput attackInput)
    {
        if (ParticlePrefab)
        {
            ParticleSystem spawnedParticle = Instantiate(ParticlePrefab, attackInput.AttackOrigin.position, attackInput.AttackOrigin.rotation);
            spawnedParticle.Play();

            foreach (BaseDamageable enemy in attackInput.Targets)
            {
                enemy.Damage(Damage);
            }

            Destroy(spawnedParticle, 5.0f);
        }
    }
}
