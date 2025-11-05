using System.Collections.Generic;
using UnityEngine;

public abstract class RangedTower : Tower
{
    [SerializeField] private Material rangeMaterial;
    [SerializeField] private LayerMask detectionLayer;

    private GameObject _rangeSphere;
    private MeshRenderer _rangeMesh;
    private TriggerDetector _triggerDetector;


    protected override void Initialize()
    {
        SetupRangeSphere();
    }

    private void SetupRangeSphere()
    {
        _rangeSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _rangeSphere.transform.SetParent(transform);
        _rangeSphere.name = "RangedTower - RangeIndicator";
        _rangeSphere.transform.localPosition = Vector3.zero;
        _rangeSphere.transform.localScale = new Vector3(towerData.Range, towerData.Range, towerData.Range);

        _rangeMesh = _rangeSphere.GetComponent<MeshRenderer>();
        _rangeMesh.material = rangeMaterial;

        _triggerDetector = _rangeSphere.AddComponent<TriggerDetector>();
    }

    public override void ShowVisuals()
    {
        base.ShowVisuals();

        _rangeMesh.enabled = true;
    }

    public override void HideVisuals()
    {
        base.HideVisuals();

        _rangeMesh.enabled = false;
    }

    protected override void OnEnabled()
    {
        _triggerDetector.OnTriggerEnterDetected += TriggerEnterDetected;
        _triggerDetector.OnTriggerExitDetected += TriggerExitDetected;
    }

    protected override void OnDisabled()
    {
        _triggerDetector.OnTriggerEnterDetected -= TriggerEnterDetected;
        _triggerDetector.OnTriggerExitDetected -= TriggerExitDetected;
    }

    protected virtual void TriggerEnterDetected(Collider other) { }
    protected virtual void TriggerExitDetected(Collider other) { }
}
