using UnityEngine;

public class BuilderManager
{
    private int _groundLayer;

    private Tower _tower;
    private TowerGroup _builtTowers = new();

    public BuilderManager(int groundLayer)
    {
        _groundLayer = groundLayer;
    }

    public void SetNewTower(Tower newTower, out BaseTowerData towerDataResult)
    {
        if (_tower == null)
            towerDataResult = null;
        else
            towerDataResult = _tower.TowerData;

        if (newTower == null) return;

        ClearTower();

        _tower = newTower;
        _tower.SetLayer(true);

        ShowSpaceOnBuilds(true);

        towerDataResult = _tower.TowerData;
    }

    private void ShowSpaceOnBuilds(bool isVisible)
    {
        if (isVisible)
            _builtTowers.ShowVisuals();
        else
            _builtTowers.HideVisuals();
    }

    private void ClearTower()
    {
        _tower = null;
        ShowSpaceOnBuilds(false);
    }

    public bool TryClearTower(out Tower clearedTower)
    {
        clearedTower = _tower;

        if (_tower)
        {
            ClearTower();
            return true;
        }

        return false;
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

    public void RemoveTower(Tower towerToSell)
    {
        _builtTowers.Remove(towerToSell);
    }

    public void UpgradeTower(Tower towerToUpgrade)
    {
        if (towerToUpgrade != null)
        {
            
        }
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
