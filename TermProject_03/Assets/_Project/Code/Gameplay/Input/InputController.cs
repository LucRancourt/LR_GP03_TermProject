using UnityEngine;
using UnityEngine.InputSystem;
using System;

using _Project.Code.Core.Events;
using _Project.Code.Core.ServiceLocator;


public class InputController : MonoBehaviourService
{
    private PlayerInputActions _inputActions;

    // [SerializeField] private InputProfile _profile;

    // public InputProfile Profile => _profile;

    #region Actions
    public event Action<Vector2> MoveEvent;

        // Camera Movement Events
    public event Action<Vector2> LookEvent;
    public event Action<bool> CameraRotateEvent;
    public event Action<float> ZoomEvent;

        // Tower Selecting/Placing Events
    public event Action<int> HotbarItemSelectedEvent;
    public event Action ClickEvent;
    #endregion


    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    public void OnEnable()
    {
        _inputActions.Gameplay.Move.performed += HandleMovePerformed;
        _inputActions.Gameplay.Move.canceled += HandleMoveCanceled;

        _inputActions.Gameplay.Look.performed += HandleLookPerformed;

        _inputActions.Gameplay.RotateCam.performed += HandleCameraRotatePerformed;
        _inputActions.Gameplay.Zoom.performed += HandleZoomPerformed;

        _inputActions.Gameplay.PauseGame.performed += HandlePausePerformed;

        _inputActions.Gameplay.SelectHotbarItem.performed += HandleSelectHorbarItemPerformed;
        _inputActions.Gameplay.Click.performed += HandleClickPerformed;

        _inputActions.Gameplay.Enable();

        _inputActions.UI.UnPause.performed += HandlePausePerformed;

    }

    #region Handlers
    private void HandleMovePerformed(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void HandleMoveCanceled(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(Vector2.zero);
    }

    private void HandleLookPerformed(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void HandleCameraRotatePerformed(InputAction.CallbackContext context)
    {
        CameraRotateEvent?.Invoke(context.ReadValueAsButton());
    }

    private void HandleZoomPerformed(InputAction.CallbackContext context)
    {
        ZoomEvent?.Invoke((context.ReadValue<Vector2>().normalized).y);
    }

    private void HandleSelectHorbarItemPerformed(InputAction.CallbackContext context)
    {
        int bindingIndex = context.action.GetBindingIndexForControl(context.control);
        InputBinding binding = context.action.bindings[bindingIndex];

        int hotbarSlot = -1;

        switch (binding.path)
        {
            case "<Keyboard>/1": hotbarSlot = 1; break;
            case "<Keyboard>/2": hotbarSlot = 2; break;
            case "<Keyboard>/3": hotbarSlot = 3; break;
            case "<Keyboard>/4": hotbarSlot = 4; break;
            case "<Keyboard>/5": hotbarSlot = 5; break;
        }

        if (hotbarSlot != -1)
            HotbarItemSelectedEvent?.Invoke(hotbarSlot);
    }

    private void HandleClickPerformed(InputAction.CallbackContext context)
    {
        ClickEvent?.Invoke();
    }

    private void HandlePausePerformed(InputAction.CallbackContext context)
    {
        EventBus.Instance.Publish(new PauseInputEvent());
    }
    #endregion

    #region Action Enablers/Disablers 
    public void EnableGameplayActions()
    {
        _inputActions.Gameplay.Enable();
        _inputActions.UI.Disable();
    }

    public void EnableUIActions()
    {
        _inputActions.Gameplay.Disable();
        _inputActions.UI.Enable();
    }

    public void DisableAllActions()
    {
        _inputActions.Gameplay.Disable();
        _inputActions.UI.Disable();
    }
    #endregion

    public void OnDisable()
    {
        if (_inputActions != null)
        {
            _inputActions.Gameplay.Disable();

            _inputActions.Gameplay.Move.performed -= HandleMovePerformed;
            _inputActions.Gameplay.Move.canceled -= HandleMoveCanceled;

            _inputActions.Gameplay.Look.performed -= HandleLookPerformed;

            _inputActions.Gameplay.RotateCam.performed -= HandleCameraRotatePerformed;
            _inputActions.Gameplay.Zoom.performed -= HandleZoomPerformed;

            _inputActions.Gameplay.PauseGame.performed -= HandlePausePerformed;

            _inputActions.Gameplay.SelectHotbarItem.performed -= HandleSelectHorbarItemPerformed;
            _inputActions.Gameplay.Click.performed -= HandleClickPerformed;

            _inputActions.UI.UnPause.performed -= HandlePausePerformed;
        }
    }
}