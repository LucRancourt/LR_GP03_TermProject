public class BaseAttackDecorator : IAttackDecorator
{
    protected IAttackDecorator wrapped;


    public BaseAttackDecorator(IAttackDecorator effect) { wrapped = effect; }

    public virtual void Apply(Enemy enemy)
    {
        wrapped?.Apply(enemy);
    }
}
