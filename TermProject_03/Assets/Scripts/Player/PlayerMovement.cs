using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerController))]

public class PlayerMovement : MonoBehaviour
{
    // Variables
    private CharacterController _characterController;
    private PlayerController _playerController;

    #region Move Vars
        private Vector3 _moveDirection;

        private Vector3 _moveVelocity;
        private Vector3 _targetMoveVelocity;

        [Header("Movement")]
        [SerializeField, Min(0.0f)] private float targetMoveSpeed = 12.0f;
    #endregion
    
    #region Look Vars
        private Camera _mainCamera;

        private Vector2 _currentMouseDelta;
        private Vector2 _currentMouseVelocity;

        private float _lookTargetRotX;

        private Vector3 _targetZoom;
        private Vector3 _currentZoomVelocity;

        [Header("Camera Rotation")]
        [SerializeField, Min(0.01f)] private float lookSpeed = 0.25f;
        [SerializeField, Min(0.01f)] private float lookSmoothTime = 0.05f;

        [SerializeField, Min(0.0f)] private float xCameraBounds = 80.0f;

        [Header("Camera Zoom")]
        [SerializeField, Min(0.01f)] private float zoomSpeed = 0.5f;
        [SerializeField, Min(0.01f)] private float zoomSmoothTime = 0.05f;
        [SerializeField, Min(0.0f)] private float zoomStrength = 100.0f;
    #endregion


    // Functions
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _characterController.detectCollisions = false;

        _playerController = GetComponent<PlayerController>();

        _mainCamera = GetComponentInChildren<Camera>();

        if (_mainCamera == null)
            Debug.LogError("MainCamera is not a Child of the Player!");
    }

    private void Update()
    {
        Move();
        Look();
        Zoom();

        if (_playerController.IsRotatingCam)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

        _moveVelocity = _targetMoveVelocity + _targetZoom;
        _characterController.Move(_moveVelocity * Time.deltaTime);
    }

    private void Move()
    {
        _moveDirection = transform.forward * _playerController.MoveInput.y + transform.right * _playerController.MoveInput.x;
        _moveDirection.Normalize();

        _targetMoveVelocity = _moveDirection * targetMoveSpeed;
    }

    private void Look()
    {
        if (!_playerController.IsRotatingCam) return;


        Vector2 targetDelta = _playerController.LookInput * lookSpeed;

        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetDelta, ref _currentMouseVelocity, lookSmoothTime);

        _lookTargetRotX = MyUtils.Clamp(_lookTargetRotX - _currentMouseDelta.y, -xCameraBounds, xCameraBounds);

        transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + _currentMouseDelta.x, 0.0f);
        _mainCamera.transform.localRotation = Quaternion.Euler(_lookTargetRotX, 0.0f, 0.0f);
    }

    private void Zoom()
    {
        Vector3 targetZoom = _mainCamera.transform.forward * _playerController.ZoomInput.y * zoomStrength * zoomSpeed;

        _targetZoom = Vector3.SmoothDamp(_targetZoom, targetZoom, ref _currentZoomVelocity, zoomSmoothTime);
    }
}
