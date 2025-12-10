using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]

public class StatusEffectSystem : MonoBehaviour
{
    private Enemy _enemy;
    private List<StatusEffectInstance> _effects = new();


    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        for (int i = _effects.Count - 1; i >= 0; i--)
        {
            StatusEffectInstance instance = _effects[i];

            instance.StatusEffect.OnTick(_enemy, instance);

            if (instance.IsCompleted)
            {
                instance.StatusEffect.OnEnd(_enemy);
                _effects.RemoveAt(i);
            }
        }
    }

    public void AddEffect(BaseStatusEffect statusEffect)
    {
        StatusEffectInstance instance;

        if (!statusEffect.IsStackable)
        {
            instance = _effects.Find(x => x.StatusEffect == statusEffect);

            if (instance != null)
            {
                instance.ResetTimeAlive();
                return;
            }
        }


        instance = new StatusEffectInstance(statusEffect);

        statusEffect.OnStart(_enemy);
        _effects.Add(instance);
    }
}