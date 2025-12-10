using _Project.Code.Core.Pool;
using System;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour, IProjectile//, IPoolable
{
    //public abstract event Action OnTargetReached;


    public abstract void Initialize(Vector3 startPos, Enemy target, float damage);

    /*
    public virtual void OnCreateForPool()
    {
    }

    public virtual void OnReturnToPool()
    {
    }

    public virtual void OnSpawnFromPool()
    {
    }*/
}
