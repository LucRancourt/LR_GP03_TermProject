using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

using _Project.Code.Core.ServiceLocator;

[RequireComponent(typeof(PlayerHotbarController))]


public class PlayerTowerMediator : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask towerModelLayer;

    private PlayerHotbarController _playerInventory;
    private TowerManager _towerManager;
    private BuilderManager _builderManager;

    private BaseTowerData _newTowerData;
    private Tower _selectedTower;


    private void Awake()
    {
        _playerInventory = GetComponent<PlayerHotbarController>();
        _towerManager = new TowerManager(_playerInventory.GetTowerList());
        _builderManager = new BuilderManager(groundLayer);

        _playerInventory.OnTowerSelected += SpawnTower;
    }

    private void Start()
    {
        ServiceLocator.Get<InputController>().HotbarItemSelectedEvent += SpawnTower;
        ServiceLocator.Get<InputController>().ClickEvent += ClickCallback;

        TowerUIWindow.Instance.OnUpgrade += UpgradeSelectedTower;
        TowerUIWindow.Instance.OnSell += SellSelectedTower;
    }

    private void UpgradeSelectedTower()
    {
        if (!_selectedTower.CanUpgrade()) return;

        int upgradeCost = _selectedTower.GetUpgradeCost();

        if (!PlayerWallet.Instance.SufficientFunds(upgradeCost)) return;

        PlayerWallet.Instance.MakeTransaction(upgradeCost);
        _selectedTower.UpgradeTower();
        TowerUIWindow.Instance.UpdateDisplay(_selectedTower, _towerManager.GetCurrentUnitLimit(_selectedTower.TowerData));
    }

    private void SellSelectedTower()
    {
        PlayerWallet.Instance.AddToWallet(_selectedTower.TowerData.GetTowerTierData(_selectedTower.TowerTier).SellValue, false);
        _builderManager.RemoveTower(_selectedTower);
        _towerManager.DespawnTower(_selectedTower);

        ClearSelectedTower();
    }

    private void SpawnTower(BaseTowerData towerData)
    {
        if (towerData == null) return;

        ClearSelectedTower();

        if (_newTowerData == towerData)
        {
            ClearInBuildTower();
            _newTowerData = null;
            return;
        }

        if (!PlayerWallet.Instance.SufficientFunds(towerData.GetPlacementCost())) return;

        _builderManager.SetNewTower(_towerManager.SpawnTower(towerData), out _newTowerData);
    }

    private void SpawnTower(int index)
    {
        BaseTowerData towerData = _playerInventory.GetTowerData(index);

        SpawnTower(towerData);
    }


    private void ClickCallback() { StartCoroutine(CheckClick()); }

    private IEnumerator CheckClick()
    {
        yield return null;

        if (EventSystem.current.IsPointerOverGameObject()) yield break;

        if (_newTowerData != null)
        {
            PlaceTower();
        }
        else
        {
            ClearSelectedTower();

            if (CameraToMouseRaycast.TryRaycastWithComponent(towerModelLayer, out _selectedTower))
            {
                _selectedTower.ShowVisuals();
                TowerUIWindow.Instance.UpdateDisplay(_selectedTower, _towerManager.GetCurrentUnitLimit(_selectedTower.TowerData));
                TowerUIWindow.Instance.Show();
            }
        }
    }

    private void ClearInBuildTower()
    {
        if (_builderManager.TryClearTower(out Tower clearedTower))
            _towerManager.DespawnTower(clearedTower);
    }

    private void ClearSelectedTower()
    {
        if (_selectedTower != null)
        {
            if (TowerUIWindow.Instance)
                TowerUIWindow.Instance.Hide();

            _selectedTower.HideVisuals();
            _selectedTower = null;
        }
    }

    private void PlaceTower()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            ClearInBuildTower();
        else
        {
            if (_builderManager.TryBuildTower())
            {
                PlayerWallet.Instance.MakeTransaction(_newTowerData.GetPlacementCost());
                _newTowerData = null;
            }
        }
    }

    private void Update()
    {
        _builderManager.Update();
    }

    private void OnDestroy()
    {
        ClearSelectedTower();

        if (ServiceLocator.TryGet(out InputController inputController))
        {
            ServiceLocator.Get<InputController>().HotbarItemSelectedEvent -= SpawnTower;
            ServiceLocator.Get<InputController>().ClickEvent -= ClickCallback;
        }

        if (TowerUIWindow.Instance)
        {
            TowerUIWindow.Instance.OnUpgrade -= UpgradeSelectedTower;
            TowerUIWindow.Instance.OnSell -= SellSelectedTower;
        }

        if (_playerInventory)
        {
            _playerInventory.OnTowerSelected -= SpawnTower;
        }
    }
}
