using _Project.Code.Core.ServiceLocator;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerWallet))]

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    private PlayerInventory _playerInventory;
    private TowerManager _towerManager;
    private BuilderManager _builderManager;

    private TowerData _newTowerData;


    private void Awake()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        _towerManager = new TowerManager(_playerInventory.GetTowerList());
        _builderManager = new BuilderManager(groundLayer);

        _playerInventory.OnTowerSelected += SpawnTower;
    }

    private void Start()
    {
        ServiceLocator.Get<InputController>().HotbarItemSelectedEvent += SpawnTower;
        ServiceLocator.Get<InputController>().ClickEvent += PlaceTowerCallback;
    }

    private void SpawnTower(TowerData towerData)
    {
        if (towerData == null) return;
        
        if (_newTowerData == towerData)
        {
            _builderManager.ClearTower();
            _newTowerData = null;
            return;
        }

        _newTowerData = towerData;

        _builderManager.SetNewTower(_towerManager.SpawnTower(_newTowerData));
    }

    private void SpawnTower(int index)
    {
        TowerData towerData = _playerInventory.GetTowerData(index);

        SpawnTower(towerData);
    }

    private void PlaceTowerCallback() { StartCoroutine(PlaceTower()); }

    private IEnumerator PlaceTower()
    {
        yield return null;

        if (EventSystem.current.IsPointerOverGameObject())
            _builderManager.ClearTower();
        else
        {
            _builderManager.BuildTower();
            _newTowerData = null;
        }
    }

    private void Update()
    {
        _builderManager.Update();
    }
}
