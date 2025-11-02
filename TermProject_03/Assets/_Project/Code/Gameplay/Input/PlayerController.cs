using UnityEngine;

using _Project.Code.Core.ServiceLocator;
//[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(PlayerMovement))]

public class PlayerController : MonoBehaviour
{
    // Variables
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsRotatingCam { get; private set; }
    public float ZoomInput { get; private set; }

    [Header("Interactable")]
    [SerializeField] private LayerMask interactLayer;


    // Functions
    private void Awake()
    {
        Cursor.visible = true;
    }

    #region InputController - OnEnable/OnDisable
    private void OnEnable()
    {
        if (ServiceLocator.TryGet(out InputController inputController))
        {
            inputController.MoveEvent += HandleMoveInput;

            inputController.LookEvent += HandleLookInput;
            inputController.CameraRotateEvent += HandleRotateCamInput;
            inputController.ZoomEvent += HandleZoomInput;
        }
    }

    private void OnDisable()
    {
        if (ServiceLocator.TryGet(out InputController inputController))
        {
            inputController.MoveEvent -= HandleMoveInput;

            inputController.LookEvent -= HandleLookInput;
            inputController.CameraRotateEvent -= HandleRotateCamInput;
            inputController.ZoomEvent -= HandleZoomInput;
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

    private void HandleZoomInput(float zoom)
    {
        ZoomInput = zoom;
    }
    #endregion
}