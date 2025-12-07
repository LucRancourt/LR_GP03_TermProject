using System;
using UnityEngine;

using _Project.Code.Core.Pool;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public abstract class Tower : MonoBehaviour, IPoolable, ITower
{
    public BaseTowerData TowerData { get; private set; }
    public int TowerTier { get; private set; } = 0;

    protected bool bHasBeenInitialized { get; private set; } = false;
    [SerializeField] private int towerSpaceLayer;
    [SerializeField] private int towerModelLayer;
    [SerializeField] private int buildingLayer;

    private GameObject _spaceTakenObject;
    private SpriteRenderer _spaceTakenCircle;
    [SerializeField] private Sprite spaceTakenSprite;
    private float _spaceTakenYRotation = 0.0f;
    private float _spaceTakenRotationSpeed = 10.0f;
    private float _spaceTakenSizeAdjustment = 1.0f;
    [SerializeField] private Material spaceMat;
    [SerializeField] private Material spaceOverlapMat;

    private bool _isOverlapping;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    [SerializeField] private LayerMask spaceLayer;

    public event Action<Tower> OnDespawned;
    public event Action OnUpgraded;


    public void OnClick() { }

    public virtual void ShowVisuals()
    {
        ShowSpaceTaken();
    }

    public void ShowSpaceTaken()
    {
        _spaceTakenCircle.enabled = true;
    }

    public virtual void HideVisuals()
    {
        _spaceTakenCircle.enabled = false;
    }

    public void Initialize(BaseTowerData data)
    {
        TowerData = data;

        if (bHasBeenInitialized) return;

        SetupSpaceTaken();


        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();

        Initialize();

        SetLayer(false);

        bHasBeenInitialized = true;
    }

    private void SetupSpaceTaken()
    {
        _spaceTakenObject = new GameObject();
        _spaceTakenObject.AddComponent<SphereCollider>().radius = 1.0f;
        _spaceTakenObject.name = "SpaceTaken Collider";
        _spaceTakenObject.transform.SetParent(transform);
        _spaceTakenObject.transform.localPosition = new Vector3(0.0f, 0.1f, 0.0f);
        _spaceTakenObject.transform.localRotation = Quaternion.Euler(90, _spaceTakenYRotation, 0);
        _spaceTakenObject.transform.localScale = new Vector3(TowerData.SpaceTaken, TowerData.SpaceTaken, TowerData.SpaceTaken);

        _spaceTakenCircle = _spaceTakenObject.AddComponent<SpriteRenderer>();
        _spaceTakenCircle.drawMode = SpriteDrawMode.Sliced;
        _spaceTakenCircle.sprite = spaceTakenSprite;
    }

    protected abstract void Initialize();

    protected virtual void OnEnabled() { }
    protected virtual void OnDisabled() { }

    public void OnCreateForPool() { }
    public void OnSpawnFromPool()
    {
        if (bHasBeenInitialized)
        {
            ShowVisuals();
            OnEnabled();

            ResetTierTowerData();
        }
    }

    public void OnReturnToPool()
    {
        if (bHasBeenInitialized)
        {
            HideVisuals();
            OnDisabled();

            ResetTierTowerData();

            SetLayer(false);
            OnDespawned?.Invoke(this);
        }
    }

    private void ResetTierTowerData()
    {
        TowerTier = 0;
        SetMesh();
    }

    protected virtual void Update()
    {
        if (!bHasBeenInitialized) return;

        RotateSpaceTakenCircle();
        CheckSpaceOverlap();
    }

    private void RotateSpaceTakenCircle()
    {
        _spaceTakenYRotation += Time.deltaTime * _spaceTakenRotationSpeed;
        _spaceTakenObject.transform.localRotation = Quaternion.Euler(90.0f, _spaceTakenYRotation, 0.0f);
    }

    private void CheckSpaceOverlap()
    {
        if (gameObject.layer == towerModelLayer) return;

        if (Physics.CheckSphere(transform.position, TowerData.SpaceTaken * _spaceTakenSizeAdjustment, spaceLayer))
        {
            _isOverlapping = true;
            _spaceTakenCircle.material = spaceOverlapMat;
        }
        else
        {
            _isOverlapping = false;
            _spaceTakenCircle.material = spaceMat;
        }
    }

    public bool CanPlace()
    {
        return !_isOverlapping;
    }

    public void SetLayer(bool isBeingBuilt)
    {
        if (isBeingBuilt)
        {
            gameObject.layer = buildingLayer;
            _spaceTakenObject.layer = buildingLayer;
            OnDisabled();
        }
        else
        {
            gameObject.layer = towerModelLayer;
            _spaceTakenObject.layer = towerSpaceLayer;
            _spaceTakenCircle.material = spaceMat;
            OnEnabled();
        }
    }


    public bool CanUpgrade() { return TowerTier + 1 < TowerData.TowerTiers.Count; }

    public int GetUpgradeCost() { return TowerData.TowerTiers[TowerTier + 1].Cost; }

    public void UpgradeTower()
    {
        TowerTier++;
        SetMesh(); 
        OnUpgraded?.Invoke();
    }

    private void SetMesh()
    {
        _meshFilter.sharedMesh = TowerData.TowerTiers[TowerTier].Model.GetComponent<MeshFilter>().sharedMesh;
        _meshRenderer.sharedMaterials = TowerData.TowerTiers[TowerTier].Model.GetComponent<MeshRenderer>().sharedMaterials;
    }
}
