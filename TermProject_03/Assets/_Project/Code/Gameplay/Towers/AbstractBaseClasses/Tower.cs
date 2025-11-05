using System;
using UnityEngine;

using _Project.Code.Core.Pool;


public abstract class Tower : MonoBehaviour, IPoolable, IClickable
{
    protected TowerData towerData;
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

    public event Action<Tower, TowerData> OnDespawned;
    public event Action<Tower, TowerData> OnClicked;



    public virtual void ShowVisuals()
    {
        OnClicked?.Invoke(this, towerData);
        ShowSpaceTaken(true);
    }

    public void ShowSpaceTaken(bool isVisible)
    {
        _spaceTakenCircle.enabled = isVisible;
    }

    public virtual void HideVisuals()
    {
        ShowSpaceTaken(false);
    }

    public void Initialize(TowerData data)
    {
        towerData = data;

        if (_hasBeenInitialized) return;

        _spaceTakenObject = new GameObject();
        _spaceTakenObject.AddComponent<SphereCollider>().radius = 2.3f;
        _spaceTakenObject.name = "SpaceTaken Collider";
        _spaceTakenObject.transform.SetParent(transform);
        _spaceTakenObject.transform.localPosition = new Vector3(0.0f, 0.1f, 0.0f);
        _spaceTakenObject.transform.localRotation = Quaternion.Euler(90, _spaceTakenYRotation, 0);
        _spaceTakenObject.transform.localScale = new Vector3(towerData.SpaceTaken, towerData.SpaceTaken, towerData.SpaceTaken);

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
        OnDespawned?.Invoke(this, towerData);
    }

    protected virtual void OnEnabled() { }
    protected virtual void OnDisabled() { }

    public void OnCreateForPool() { }
    public void OnSpawnFromPool()
    {
        if (_hasBeenInitialized)
            OnEnabled();
    }

    public void OnReturnToPool()
    {
        if (_hasBeenInitialized)
            OnDisabled();
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

        if (Physics.CheckSphere(transform.position, towerData.SpaceTaken * 1.2f, spaceLayer))
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
