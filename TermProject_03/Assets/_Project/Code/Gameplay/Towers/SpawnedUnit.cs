using UnityEngine;



//  NEEDS TO HAVE A SHARED PARENT? WITH ENEMY - TOO MANY SIMILARITIES



[RequireComponent(typeof(PathNavigator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class SpawnedUnit : MonoBehaviour
{
    // Variables
    [SerializeField] private SpawnedUnitConfig spawnedUnitConfig;
    private GameObject _model;
    private PathNavigator _pathNavigator;
    private HealthController _healthController;


}
