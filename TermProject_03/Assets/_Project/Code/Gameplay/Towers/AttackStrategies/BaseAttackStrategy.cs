using _Project.Code.Core.Audio;
using _Project.Code.Core.Strategy;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttackStrategy : ScriptableStrategy<AttackInput>
{
    public float Damage;
    public AudioCue ShootSFX;
    public BaseAttackDecorator[] Effects;

    public override void Execute(AttackInput attackInput) 
    {
        if (ShootSFX)
            AudioManager.Instance.PlaySound(ShootSFX);
    }
}

public class AttackInput
{
    public Transform AttackOrigin;
    public List<Enemy> Targets;

    public AttackInput(Transform attackOrigin, List<Enemy> targets)
    {
        AttackOrigin = attackOrigin;
        Targets = targets;
    }
}