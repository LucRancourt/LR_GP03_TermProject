using _Project.Code.Core.General;

public class FreezeEffect : BaseAttackDecorator
{
    private float _strength;
    private float _duration;


    public FreezeEffect(IAttackDecorator baseEffect, float strength, float duration) : base(baseEffect)
    {
        _strength = MyUtils.Clamp(strength, 0.0f, 1.0f);
        _duration = duration;
    }

    public override void Apply(Enemy enemy)
    {
        base.Apply(enemy);
        //enemy.ApplyFreeze(_strength, _duration);
    }
}
