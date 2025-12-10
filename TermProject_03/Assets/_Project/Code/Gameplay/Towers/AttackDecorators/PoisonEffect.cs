public class PoisonEffect : BaseAttackDecorator
{
    private float _damagePerSecond;
    private float _duration;


    public PoisonEffect(IAttackDecorator baseEffect, float damagePerSecond, float duration) : base(baseEffect)
    {
        _damagePerSecond = damagePerSecond;
        _duration = duration;
    }

    public override void Apply(Enemy enemy)
    {
        base.Apply(enemy);
        //enemy.ApplyPoison(_damagePerSecond, _duration);
    }
}
