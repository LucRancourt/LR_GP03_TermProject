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
    [SerializeField] private LayerMask towerModelMask;

    private Outline _hoverOutline = null;


    public void OnClick() { }

    public virtual void ShowVisuals()
    {
        ShowSpaceTaken();
    }

    public void ShowSpaceTaken()
    {
        _spaceTakenCircle.enabled = true;
        OnHoverExit();
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
        _spaceTakenObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        _spaceTakenObject.name = "SpaceTaken Collider";
        _spaceTakenObject.transform.SetParent(transform);
        _spaceTakenObject.transform.localPosition = new Vector3(0.0f, 0.1f, 0.0f);
        _spaceTakenObject.transform.localRotation = Quaternion.Euler(90, _spaceTakenYRotation, 0);
        _spaceTakenObject.transform.localScale = new Vector3(TowerData.SpaceTaken, TowerData.SpaceTaken, TowerData.SpaceTaken);

        _spaceTakenCircle = _spaceTakenObject.AddComponent<SpriteRenderer>();
        _spaceTakenCircle.drawMode = SpriteDrawMode.Sliced;
        _spaceTakenCircle.sprite = spaceTakenSprite;

        SetupOutline();
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
        }
    }

    private void ResetTierTowerData()
    {
        TowerTier = 0;
        SetMesh();

        SetupOutline();
    }

    private static Tower _currentHoveredTower;

    protected virtual void Update()
    {
        if (!bHasBeenInitialized) return;

        RotateSpaceTakenCircle();
        CheckSpaceOverlap();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100000000.0f, towerModelMask))
        {
            Tower towerHit = hit.collider.GetComponent<Tower>();

            if (towerHit != _currentHoveredTower)
            {
                _currentHoveredTower?.OnHoverExit();
                _currentHoveredTower = towerHit;
                _currentHoveredTower?.OnHoverEnter();
            }
        }
        else if (_currentHoveredTower != null)
        {
            _currentHoveredTower.OnHoverExit();
            _currentHoveredTower = null;
        }
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
        UpdateDataOnUpgrade();
    }

    protected virtual void UpdateDataOnUpgrade()
    {
        TowerTier++;
        SetMesh();

        SetupOutline();
    }

    private void SetMesh()
    {
        _meshFilter.sharedMesh = TowerData.TowerTiers[TowerTier].Model.GetComponent<MeshFilter>().sharedMesh;
        _meshRenderer.sharedMaterials = TowerData.TowerTiers[TowerTier].Model.GetComponent<MeshRenderer>().sharedMaterials;
    }


    private void OnHoverEnter() { if (_spaceTakenCircle.enabled == true) return; _hoverOutline.enabled = true; }
    private void OnHoverExit() { _hoverOutline.enabled = false; }

    private void SetupOutline()
    {
        if (_hoverOutline != null)
        {
            Destroy(_hoverOutline);
            _hoverOutline = null;
            Invoke("SetupOutline", 0.1f);
            return;
        }

        _hoverOutline = gameObject.AddComponent<Outline>();
        _hoverOutline.OutlineMode = Outline.Mode.OutlineAll;
        _hoverOutline.OutlineColor = Color.white;
        _hoverOutline.OutlineWidth = 5f;
        _hoverOutline.enabled = false;
    }
}
