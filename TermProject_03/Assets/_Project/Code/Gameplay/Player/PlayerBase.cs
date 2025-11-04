using _Project.Code.Core.ServiceLocator;
using UnityEngine;

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
        ServiceLocator.Get<InputController>().ClickEvent += PlaceTower;
    }

    private void SpawnTower(TowerData towerData)
    {
        if (towerData == null) return;

        _builderManager.SetNewTower(_towerManager.SpawnTower(towerData));
    }

    private void SpawnTower(int index)
    {
        TowerData towerData = _playerInventory.GetTowerData(index);

        if (towerData == null) return;

        _builderManager.SetNewTower(_towerManager.SpawnTower(towerData));
    }

    private void PlaceTower()
    {
        _builderManager.BuildTower();
    }

    private void Update()
    {
        _builderManager.Update();
    }
}
