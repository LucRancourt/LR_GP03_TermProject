using _Project.Code.Core.General;
using System;
using System.Collections;
using UnityEngine;

public class PathNavigator : MonoBehaviour
{
    private NavPath _path;
    private int _currentWaypointIndex;

    private float _moveSpeed;

    private bool _isMoving;
    private bool _autoPlayOnEnable = true;


    private void OnEnable()
    {
        if (!_autoPlayOnEnable) return;

        PlayPath();
    }

    private void OnDisable()
    {
        StopPath();
    }

    public void SetupPath(NavPath navPath, float moveSpeed, bool isReversed)
    {
        _path = navPath;
        _moveSpeed = moveSpeed;

        if (isReversed)
            _path.ReversePath();
    }

    public void PlayPath()
    {
        if (!_isMoving && _path != null)
        {
            ResetPath();
            _isMoving = true;
            StartCoroutine(MoveAlongPath());
        }
    }

    public void StopPath()
    {
        _isMoving = false;
    }

    private IEnumerator MoveAlongPath()
    {
        while (_isMoving && _currentWaypointIndex < _path.GetWaypointCount() - 1)
        {
            Vector3 end = _path.GetWaypoints()[_currentWaypointIndex + 1];

            yield return StartCoroutine(MoveTowardsTarget(end, _moveSpeed));

            _currentWaypointIndex++;
        }

        _isMoving = false;

        StopPath();
    }

    public IEnumerator MoveTowardsTarget(Vector3 targetPosition, float speed, float stopDistance = 0.15f)
    {
        Vector3 direction = MyUtils.GetDirection(targetPosition, transform.position);
        transform.rotation = Quaternion.LookRotation(direction);

        while (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }
    }

    private void ResetPath()
    {
        _currentWaypointIndex = 0;

        transform.position = _path.GetWaypoints()[0];
    }
}