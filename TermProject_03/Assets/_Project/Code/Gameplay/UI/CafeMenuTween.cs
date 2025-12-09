using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class CafeMenuTween : MonoBehaviour
{
    [SerializeField] List<Transform> objectsToAnimate;
    [SerializeField] List<Transform> targetTransform;

    [SerializeField] float duration = 1f;

    public event Action OnTransformEnd;

    private Vector3[] _originalPositions;
    private Quaternion[] _originalRotations;

    private Vector3[] _targetPositions;
    private Quaternion[] _targetRotations;

    private bool _wasInitialized = false;


    private void Start()
    {
        _targetPositions = new Vector3[targetTransform.Count];
        _targetRotations = new Quaternion[targetTransform.Count];

        for (int i = 0; i < targetTransform.Count; i++)
        {
            _targetPositions[i] = targetTransform[i].position;
            _targetRotations[i] = targetTransform[i].rotation;
        }
    }

    public void PlayForward()  
    {
        if (!_wasInitialized)
        {
            _wasInitialized = true;

            _originalPositions = new Vector3[objectsToAnimate.Count];
            _originalRotations = new Quaternion[objectsToAnimate.Count];

            for (int i = 0; i < objectsToAnimate.Count; i++)
            {
                _originalPositions[i] = objectsToAnimate[i].position;
                _originalRotations[i] = objectsToAnimate[i].rotation;
            }
        }

        StopAllCoroutines();
        StartCoroutine(Animate(objectsToAnimate, _targetPositions, _targetRotations, duration));
    }

    public void PlayBackward()  
    {
        if (!_wasInitialized) return;

        StopAllCoroutines();
        StartCoroutine(Animate(objectsToAnimate, _originalPositions, _originalRotations, duration));
    }

    IEnumerator Animate(List<Transform> objects, Vector3[] targetPos, Quaternion[] targetRot, float duration)
    {
        float time = 0f;

        Vector3[] startPos = new Vector3[objects.Count];
        Quaternion[] startRot = new Quaternion[objects.Count];

        for (int i = 0; i < objects.Count; i++)
        {
            startPos[i] = objects[i].position;
            startRot[i] = objects[i].rotation;
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            float lerpTime = Mathf.Clamp01(time / duration);

            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].position = Vector3.Lerp(startPos[i], targetPos[i], lerpTime);
                objects[i].rotation = Quaternion.Slerp(startRot[i], targetRot[i], lerpTime);
            }

            yield return null;
        }

        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].SetPositionAndRotation(targetPos[i], targetRot[i]);
        }


        OnTransformEnd?.Invoke();
    }
}