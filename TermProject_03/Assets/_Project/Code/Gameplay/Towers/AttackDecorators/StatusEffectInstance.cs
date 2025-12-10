using System;
using UnityEngine;

public class StatusEffectInstance
{
    public BaseStatusEffect StatusEffect { get; private set; }
    private float _timeAlive;
    public bool IsCompleted => _timeAlive >= StatusEffect.Duration;


    public StatusEffectInstance(BaseStatusEffect baseStatusEffect)
    {
        StatusEffect = baseStatusEffect;
        ResetTimeAlive();
    }

    public void IncreaseTimeAlive() 
    { 
        _timeAlive += Time.deltaTime;
    }

    public void ResetTimeAlive() { _timeAlive = 0.0f; }
}
