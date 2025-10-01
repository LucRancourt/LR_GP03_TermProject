using DG.Tweening;
using System;
using UnityEngine;

public class PathNavigator : MonoBehaviour
{
    // Variables
    private Sequence _pathingSequence;


    // Functions
    public void SetupPath(NavPath navPath, float moveSpeed, bool isReversed)
    {
        DOTween.Init();
        DOTween.SetTweensCapacity(500, 50);

        _pathingSequence = DOTween.Sequence();


        Vector3[] path = navPath.GetWaypoints();

        if (isReversed)
            Array.Reverse(path);

        transform.position = path[0];
        Vector3 lastPos = transform.position;
        Vector3 pointPos;


        foreach (Vector3 point in navPath.GetWaypoints())
        {
            if (lastPos == point)
                continue;

            pointPos = point;

            _pathingSequence.Append(transform.DOMove(pointPos, Vector3.Distance(lastPos, pointPos) / moveSpeed).SetEase(Ease.Linear)); 
            _pathingSequence.Join(transform.DOLookAt(pointPos, 0.01f).SetEase(Ease.Linear));

            lastPos = pointPos;
        }

        _pathingSequence.AppendCallback(OnReachedPathEnd);
    }

    public void PlayPath()
    {
        if (_pathingSequence.IsActive() && !_pathingSequence.IsPlaying())
            _pathingSequence.Play();
    }

    public void OnReachedPathEnd()
    {
        Debug.Log("Reached Path End!");
        StopPath();
    }

    public void StopPath()
    {
        _pathingSequence.Kill();
    }
}
