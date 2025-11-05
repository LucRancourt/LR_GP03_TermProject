using UnityEngine;
using System.Collections.Generic;


public class BuilderManager
{
    // Variables
    private int _groundLayer;

    private Tower _tower;
    private List<Tower> _builtTowers = new();

    public BuilderManager(int groundLayer)
    {
        _groundLayer = groundLayer;
    }

    public void SetNewTower(Tower newTower)
    {
        if (newTower == null) return;

        ClearTower();

        _tower = newTower;
        _tower.SetLayer(true);

        ShowSpaceOnBuilds(true);
    }

    private void ShowSpaceOnBuilds(bool isVisible)
    {
        foreach (Tower tower in _builtTowers)
        {
            tower.ShowSpaceTaken(isVisible);
        }
    }

    public void ClearTower()
    {
        if (_tower)
        {
            _tower.DespawnTower();
            _tower = null;

            ShowSpaceOnBuilds(false);
        }
    }

    public bool TryBuildTower()
    {
        if (_tower == null) return false;

        if (_tower.CanPlace())
        {
            _tower.SetLayer(false);

            _builtTowers.Add(_tower);
            ShowSpaceOnBuilds(false);

            _tower = null;

            return true;
        }

        return false;
    }

    public void SellTower(Tower towerToSell)
    {
        _builtTowers.Remove(towerToSell);
    }

    public void Update()
    {
        if (_tower)
        {
            if(CameraToMouseRaycast.TryRaycast(_groundLayer, out RaycastHit hit))
                _tower.transform.position = hit.point;
        }
    }
}
