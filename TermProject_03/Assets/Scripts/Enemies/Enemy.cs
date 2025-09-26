using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    // Variables
    [SerializeField] private BaseEnemyConfig baseEnemyConfig;

    private float _currentHealth;

    [SerializeField] private NavPath path;


    // Functions
    public void Initialize()
    {
        _currentHealth = baseEnemyConfig.Health;

        SetupRoute();
    }

    private void SetupRoute()
    {
        DOTween.Init();

        Sequence pathingSequence = DOTween.Sequence();

        Vector3 lastPos = transform.position;
        Vector3 pointPos;

        foreach (Vector3 point in path.GetWaypoints())
        {
            pointPos = point;

            pathingSequence.Append(transform.DOMove(pointPos, Vector3.Distance(lastPos, pointPos) / baseEnemyConfig.Speed).SetEase(Ease.Linear));
            pathingSequence.Join(transform.DOLookAt(pointPos, 0.01f).SetEase(Ease.Linear));

            lastPos = pointPos;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth <= 0.0f)
            _currentHealth = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    // Remove HP on Hit

    // If 0 HP send back to Pool 

    // ^^^ Create Enemy Pool for each Level?

    // If Hit Base Collider -> Send back to Pool + remove HP from Base HP through GameManager
}
