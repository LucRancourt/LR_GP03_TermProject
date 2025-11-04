using System.Collections.Generic;
using UnityEngine;

public abstract class RangedTower : Tower
{
    [SerializeField] private Material rangeMaterial;
    [SerializeField] private LayerMask detectionLayer;

    private GameObject _rangeSphere;
    private MeshRenderer _rangeMesh;
    private TriggerDetector _triggerDetector;

    protected List<Enemy> _enemiesInRange = new List<Enemy>();


    protected override void Initialize()
    {
        _rangeSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _rangeSphere.transform.SetParent(transform);
        _rangeSphere.transform.localPosition = Vector3.zero;
        _rangeSphere.transform.localScale = new Vector3(towerData.Range, towerData.Range, towerData.Range);

        _rangeMesh = _rangeSphere.GetComponent<MeshRenderer>();
        _rangeMesh.material = rangeMaterial;

        _triggerDetector = _rangeSphere.AddComponent<TriggerDetector>();
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

    private void TriggerEnterDetected(Collider other)
    {
        if (other.TryGetComponent(out Enemy newEnemy))
            if (!_enemiesInRange.Contains(newEnemy))
                _enemiesInRange.Add(newEnemy);
    }

    private void TriggerExitDetected(Collider other)
    {
        if (other.TryGetComponent(out Enemy newEnemy))
            if (_enemiesInRange.Contains(newEnemy))
                _enemiesInRange.Remove(newEnemy);
    }

    protected void ClearListOfInactives()
    {
        for (int i = _enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (!_enemiesInRange[i].isActiveAndEnabled)
                _enemiesInRange.Remove(_enemiesInRange[i]);
        }
    }
}
