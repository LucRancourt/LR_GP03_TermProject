using UnityEngine;

using _Project.Code.Core.General;


public class BuilderManager
{
    // Variables
    private LayerMask _groundLayer;

    private Tower _tower;

    public BuilderManager(LayerMask groundLayer)
    {
        _groundLayer = groundLayer;
    }

    public void SetNewTower(Tower newTower)
    {
        if (_tower)
        {
            _tower.DespawnTower();
        }

        _tower = newTower;
    }

    public void BuildTower()
    {
        _tower = null;
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
