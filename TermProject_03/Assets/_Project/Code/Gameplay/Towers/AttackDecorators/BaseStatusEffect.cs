using UnityEngine;

public abstract class BaseStatusEffect : ScriptableObject
{
    public string Name;
    public float Duration;

    public bool IsStackable = false;


    public abstract void OnStart(Enemy enemy);
    public abstract void OnTick(Enemy enemy, StatusEffectInstance effectInstance);
    public abstract void OnEnd(Enemy enemy);
}
