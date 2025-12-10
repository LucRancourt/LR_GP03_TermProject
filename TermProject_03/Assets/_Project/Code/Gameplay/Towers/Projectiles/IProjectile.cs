using UnityEngine;

public interface IProjectile
{
    public void Initialize(Vector3 startPos, Enemy target, float damage);
}