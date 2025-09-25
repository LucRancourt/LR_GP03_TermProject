using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    // Variables
        private InputController _inputController;
        private CharacterController _characterController;

        #region Move Vars
            private Vector2 _moveInput;

            private Vector3 _moveDirection;

            private Vector3 _moveVelocity;
            private Vector3 _targetMoveVelocity;

            [Header("Movement")]
            [SerializeField, Min(0.0f)] private float targetMoveSpeed = 12.0f;
        #endregion

        #region Look Vars
            private Vector2 _lookInput;
            private bool _isRotatingCam = false;
            private Vector2 _zoomInput;

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

        [Header("Interactable")]
        [SerializeField] private LayerMask interactLayer;


    // Functions
        private void Awake()
        {
            _inputController = GetComponent<InputController>();

            _characterController = GetComponent<CharacterController>();
            _characterController.detectCollisions = false;

            _mainCamera = GetComponentInChildren<Camera>();

            if (_mainCamera == null)
                Debug.LogError("MainCamera is not a Child of the Player!");

            Cursor.visible = true;
        }

        #region InputController - OnEnable/OnDisable
            private void OnEnable()
            {
                if (_inputController != null)
                {
                    _inputController.MoveEvent += HandleMoveInput;

                    _inputController.LookEvent += HandleLookInput;
                    _inputController.RotateCamEvent += HandleRotateCamInput;
                    _inputController.ZoomEvent += HandleZoomInput;

                    _inputController.PauseGameEvent += HandlePauseGameInput;
                }
            }

            private void OnDisable()
            {
                if (_inputController != null)
                {
                    _inputController.MoveEvent -= HandleMoveInput;

                    _inputController.LookEvent -= HandleLookInput;
                    _inputController.RotateCamEvent -= HandleRotateCamInput;
                    _inputController.ZoomEvent -= HandleZoomInput;

                    _inputController.PauseGameEvent -= HandlePauseGameInput;
                }
            }
        #endregion

        #region HandleInputs
            private void HandleMoveInput(Vector2 movement)
            {
                _moveInput = movement;
            }

            private void HandleLookInput(Vector2 look)
            {
                _lookInput = look;
            }

            private void HandleRotateCamInput(bool rotateCam)
            {
                _isRotatingCam = rotateCam;

                if (_isRotatingCam)
                    Cursor.lockState = CursorLockMode.Locked;
                else
                    Cursor.lockState = CursorLockMode.None;
            }

            private void HandleZoomInput(Vector2 zoom)
            {
                _zoomInput = zoom.normalized;
            }

            private void HandlePauseGameInput()
            {

                PauseMenu.Instance.PauseGame();
            }
        #endregion


        private void Update()
        {
            Move();
            Look();
            Zoom();

            _moveVelocity = _targetMoveVelocity + _targetZoom;
            _characterController.Move(_moveVelocity * Time.deltaTime);
        }

        private void Move()
        {
            _moveDirection = transform.forward * _moveInput.y + transform.right * _moveInput.x;
            _moveDirection.Normalize();

            _targetMoveVelocity = _moveDirection * targetMoveSpeed;
        }

        private void Look()
        {
            if (!_isRotatingCam) return;


            Vector2 targetDelta = _lookInput * lookSpeed;

            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetDelta, ref _currentMouseVelocity, lookSmoothTime);

            _lookTargetRotX = MyUtils.Clamp(_lookTargetRotX - _currentMouseDelta.y, -xCameraBounds, xCameraBounds);

            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + _currentMouseDelta.x, 0.0f);
            _mainCamera.transform.localRotation = Quaternion.Euler(_lookTargetRotX, 0.0f, 0.0f);
        }

        private void Zoom()
        {
            Vector3 targetZoom = _mainCamera.transform.forward * _zoomInput.y * zoomStrength * zoomSpeed;
            
            _targetZoom = Vector3.SmoothDamp(_targetZoom, targetZoom, ref _currentZoomVelocity, zoomSmoothTime);
        }
}
