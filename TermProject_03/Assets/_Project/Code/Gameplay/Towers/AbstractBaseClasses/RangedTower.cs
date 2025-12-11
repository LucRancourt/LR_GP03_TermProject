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
        _rangeSphere.layer = LayerMask.NameToLayer("Ignore Raycast");
        _rangeSphere.name = "RangedTower - RangeIndicator";
        _rangeSphere.transform.localPosition = Vector3.zero;
        AdjustRangeSphereSize();

        _rangeMesh = _rangeSphere.GetComponent<MeshRenderer>();
        _rangeMesh.material = rangeMaterial;

        _triggerDetector = _rangeSphere.AddComponent<TriggerDetector>();
    }

    private void AdjustRangeSphereSize()
    {
        _rangeSphere.transform.localScale = new Vector3(TowerData.TowerTiers[TowerTier].Range, TowerData.TowerTiers[TowerTier].Range, TowerData.TowerTiers[TowerTier].Range);
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

        InitialRaycastOnEnable();
    }

    protected override void OnDisabled()
    {
        _triggerDetector.OnTriggerEnterDetected -= TriggerEnterDetected;
        _triggerDetector.OnTriggerExitDetected -= TriggerExitDetected;

        AdjustRangeSphereSize();
    }

    protected abstract void InitialRaycastOnEnable();
    protected Collider[] CheckAnyInRange() { return Physics.OverlapBox(transform.position, _rangeSphere.GetComponent<Collider>().bounds.extents); }

    protected abstract void TriggerEnterDetected(Collider other);
    protected abstract void TriggerExitDetected(Collider other); 

    protected override void UpdateDataOnUpgrade()
    {
        base.UpdateDataOnUpgrade();

        AdjustRangeSphereSize();
    }
}
