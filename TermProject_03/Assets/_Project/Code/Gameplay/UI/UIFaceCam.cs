using UnityEngine;

public class UIFaceCam : MonoBehaviour
{
    private Camera _mainCamera;


    private void Start()
    {
        _mainCamera = Camera.main;    
    }

    private void LateUpdate()
    {
        if (_mainCamera)
        {
            transform.LookAt(_mainCamera.transform);
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
}
