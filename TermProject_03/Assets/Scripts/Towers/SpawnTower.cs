using UnityEngine;

public class SpawnTower : Tower
{
    // Variables
    [SerializeField] private NavPath path;
    private float _cooldown;
    private Pool _spawnUnitPool;


    // Functions
    protected override void Awake()
    {
        if (_towerConfig.Type != TowerType.Spawn) Debug.LogError("Unsupported Tower Type!");

        base.Awake();


        if (_towerConfig.SpawnedUnitModel.GetComponent<PathNavigator>() && _towerConfig.SpawnedUnitModel.GetComponent<HealthSystem>())
            _spawnUnitPool = new Pool(_towerConfig.SpawnedUnitModel);
        else
            Debug.LogError("No PathNavigator/HealthSystem on SpawnedUnitModel! - Temp");  //

    }

    private void Update()
    {
        _cooldown -= Time.deltaTime;

        if (_cooldown <= 0.0f)
        {
            PathNavigator spawnedUnit = _spawnUnitPool.PullFromPool().GetComponent<PathNavigator>();
            spawnedUnit.SetupPath(path, _towerConfig.SpawnedUnitSpeed, true);
            spawnedUnit.PlayPath();

            _cooldown = _towerConfig.Rate;
        }
    }
}
