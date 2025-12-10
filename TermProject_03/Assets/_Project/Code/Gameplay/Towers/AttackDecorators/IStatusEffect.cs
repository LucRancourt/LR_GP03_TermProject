using UnityEngine;

public interface IStatusEffect
{
    public bool IsFinished { get; }

    public void Apply();
    public void Tick();
    public void Remove();
}
