using DG.Tweening;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Variables
    Sequence pathingSequence;


    // Functions
    public void SetupPath(NavPath navPath, float moveSpeed)
    {
        DOTween.Init();
        DOTween.SetTweensCapacity(500, 50);

        pathingSequence = DOTween.Sequence();

        Vector3 lastPos = MyUtils._nullVector3Value;
        Vector3 pointPos;

        foreach (Vector3 point in navPath.GetWaypoints())
        {
            if (lastPos == MyUtils._nullVector3Value)
            {
                transform.position = point;
                lastPos = point;
                continue;
            }

            pointPos = point;

            pathingSequence.Append(transform.DOMove(pointPos, Vector3.Distance(lastPos, pointPos) / moveSpeed).SetEase(Ease.Linear));
            pathingSequence.Join(transform.DOLookAt(pointPos, 0.01f).SetEase(Ease.Linear));

            lastPos = pointPos;
        }

    }

    public void PlayPath()
    {
        if (pathingSequence.IsActive() && !pathingSequence.IsPlaying())
            pathingSequence.Play();
    }

    public void StopPath()
    {
        pathingSequence.Kill();
    }
}
