using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(PlayerMovement))]

public class PlayerController : MonoBehaviour
{
    // Variables
    private InputController _inputController;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsRotatingCam { get; private set; }
    public Vector2 ZoomInput { get; private set; }

    [Header("Interactable")]
    [SerializeField] private LayerMask interactLayer;


    // Functions
    private void Awake()
    {
        _inputController = GetComponent<InputController>();

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
            MoveInput = movement;
        }

        private void HandleLookInput(Vector2 look)
        {
            LookInput = look;
        }

        private void HandleRotateCamInput(bool rotateCam)
        {
            IsRotatingCam = rotateCam;
        }

        private void HandleZoomInput(Vector2 zoom)
        {
            ZoomInput = zoom.normalized;
        }

        private void HandlePauseGameInput()
        {
            PauseMenu.Instance.PauseGame();
        }
    #endregion
}
