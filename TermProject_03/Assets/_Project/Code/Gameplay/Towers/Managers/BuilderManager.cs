using UnityEngine;

using _Project.Code.Core.General;
using System.Collections.Generic;

public class BuilderManager
{
    // Variables
    private LayerMask _groundLayer;

    private Tower _tower;
    private List<Tower> _builtTowers = new();

    public BuilderManager(LayerMask groundLayer)
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

    public void BuildTower()
    {
        if (_tower == null) return;

        if (_tower.CanPlace())
        {
            _tower.SetLayer(false);

            _builtTowers.Add(_tower);
            ShowSpaceOnBuilds(false);

            _tower = null;
        }
    }

    public void SellTower(Tower towerToSell)
    {
        _builtTowers.Remove(towerToSell);
    }

    public void Update()
    {
        if (_tower)
        {
            Vector3 pos = Input.mousePosition;
            pos.z += 5.0f;
            pos = Camera.main.ScreenToWorldPoint(pos);

            Vector3 dir = MyUtils.GetDirection(pos, Camera.main.transform.position);

            Debug.DrawLine(Camera.main.transform.position, dir * 200.0f);
            if (Physics.Raycast(Camera.main.transform.position, dir, out RaycastHit hit, 2000.0f, _groundLayer))
            {
                _tower.transform.position = hit.point;
            }
        }
    }
}
