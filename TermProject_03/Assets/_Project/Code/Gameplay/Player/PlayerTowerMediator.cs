using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

using _Project.Code.Core.ServiceLocator;

[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerWallet))]

public class PlayerTowerMediator : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask towerModelLayer;

    private PlayerInventory _playerInventory;
    private TowerManager _towerManager;
    private BuilderManager _builderManager;

    private TowerData _newTowerData;
    private Tower _selectedTower;


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
        ServiceLocator.Get<InputController>().ClickEvent += ClickCallback;
    }

    private void SpawnTower(TowerData towerData)
    {
        if (towerData == null) return;

        ClearSelectedTower();

        if (_newTowerData == towerData)
        {
            _builderManager.ClearTower();
            _newTowerData = null;
            return;
        }

        _builderManager.SetNewTower(_towerManager.SpawnTower(towerData), out _newTowerData);
    }

    private void SpawnTower(int index)
    {
        TowerData towerData = _playerInventory.GetTowerData(index);

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
                TowerUIWindow.Instance.UpdateDisplay(_selectedTower.TowerData);
                TowerUIWindow.Instance.Show();
            }
        }
    }

    private void ClearSelectedTower()
    {
        if (_selectedTower != null)
        {
            TowerUIWindow.Instance.Hide();
            _selectedTower.HideVisuals();
            _selectedTower = null;
        }
    }

    private IEnumerator HideElements()
    {
        yield return null;

        if (!EventSystem.current.IsPointerOverGameObject() && _selectedTower != null)
        {
            TowerUIWindow.Instance.Hide();
            _selectedTower.HideVisuals();
            _selectedTower = null;
        }
    }

    private void PlaceTower()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            _builderManager.ClearTower();
        else
        {
            if (_builderManager.TryBuildTower())
                _newTowerData = null;
        }
    }

    private void Update()
    {
        _builderManager.Update();
    }
}
