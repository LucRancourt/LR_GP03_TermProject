using UnityEngine;

[CreateAssetMenu(fileName = "NewIceEffect", menuName = "Scriptable Objects/Status Effects/Ice")]
public class IceEffect : BaseStatusEffect
{
    public float SlowStrength;


    public override void OnStart(Enemy enemy)
    {
        enemy.UpdateSpeed(enemy.Speed * (1 - SlowStrength));
    }

    public override void OnTick(Enemy enemy, StatusEffectInstance effectInstance)
    {
        effectInstance.IncreaseTimeAlive();
    }

    public override void OnEnd(Enemy enemy)
    {
        enemy.UpdateSpeed(enemy.Speed);
    }
}
