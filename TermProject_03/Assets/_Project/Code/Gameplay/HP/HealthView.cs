using _Project.Code.Core.MVC;
using UnityEngine;

public class HealthView : BaseView<float>
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void UpdateDisplay(float data)
    {
        Debug.Log(gameObject.name + " has " + data + " health!!!!");
    }
}


    //_healthBar = FindFirstObjectByType<Canvas>().gameObject.AddComponent<Image>();
