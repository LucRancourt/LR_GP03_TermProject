using System;
using UnityEngine;

using _Project.Code.Core.Pool;


public abstract class Tower : MonoBehaviour, IPoolable, IClickable
{
    public TowerData TowerData { get; private set; }
    private bool _hasBeenInitialized = false;
    [SerializeField] private int towerSpaceLayer;
    [SerializeField] private int towerModelLayer;
    [SerializeField] private int buildingLayer;

    private GameObject _spaceTakenObject;
    private SpriteRenderer _spaceTakenCircle;
    [SerializeField] private Sprite spaceTakenSprite;
    private float _spaceTakenYRotation = 0.0f;
    [SerializeField] private Material spaceMat;
    [SerializeField] private Material spaceOverlapMat;

    private bool _isOverlapping;


    [SerializeField] private LayerMask spaceLayer;

    public event Action<Tower> OnDespawned;


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

    public void Initialize(TowerData data)
    {
        TowerData = data;

        if (_hasBeenInitialized) return;

        _spaceTakenObject = new GameObject();
        _spaceTakenObject.AddComponent<SphereCollider>().radius = 2.3f;
        _spaceTakenObject.name = "SpaceTaken Collider";
        _spaceTakenObject.transform.SetParent(transform);
        _spaceTakenObject.transform.localPosition = new Vector3(0.0f, 0.1f, 0.0f);
        _spaceTakenObject.transform.localRotation = Quaternion.Euler(90, _spaceTakenYRotation, 0);
        _spaceTakenObject.transform.localScale = new Vector3(TowerData.SpaceTaken, TowerData.SpaceTaken, TowerData.SpaceTaken);

        _spaceTakenCircle = _spaceTakenObject.AddComponent<SpriteRenderer>();
        _spaceTakenCircle.drawMode = SpriteDrawMode.Sliced;
        _spaceTakenCircle.sprite = spaceTakenSprite;

        SetLayer(false);

        Initialize();

        _hasBeenInitialized = true;

        OnEnabled();
    }

    protected abstract void Initialize();

    public void DespawnTower()
    {
        SetLayer(false);
        OnDespawned?.Invoke(this);
    }

    protected virtual void OnEnabled() { }
    protected virtual void OnDisabled() { }

    public void OnCreateForPool() { }
    public void OnSpawnFromPool()
    {
        if (_hasBeenInitialized)
        {
            ShowVisuals();
            OnEnabled();
        }
    }

    public void OnReturnToPool()
    {
        if (_hasBeenInitialized)
        {
            HideVisuals();
            OnDisabled();
        }
    }

    protected virtual void Update()
    {
        if (!_hasBeenInitialized) return;

        RotateSpaceTakenCircle();
        CheckSpaceOverlap();
    }

    private void RotateSpaceTakenCircle()
    {
        _spaceTakenYRotation += Time.deltaTime * 10.0f;
        _spaceTakenObject.transform.localRotation = Quaternion.Euler(90, _spaceTakenYRotation, 0);
    }

    private void CheckSpaceOverlap()
    {
        if (gameObject.layer == towerModelLayer) return;

        if (Physics.CheckSphere(transform.position, TowerData.SpaceTaken * 1.2f, spaceLayer))
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
        }
        else
        {
            gameObject.layer = towerModelLayer;
            _spaceTakenObject.layer = towerSpaceLayer;
            _spaceTakenCircle.material = spaceMat;
        }
    }
}
