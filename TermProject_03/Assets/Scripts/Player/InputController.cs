using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    // Variables
        private InputControls _inputControls;

        #region Actions
            public event Action<Vector2> MoveEvent;

            // Camera Movement Events
            public event Action<Vector2> LookEvent;
            public event Action<bool> RotateCamEvent;
            public event Action<Vector2> ZoomEvent;

            public event Action PauseGameEvent;
        #endregion


    // Functions
        private void Awake()
        {
            _inputControls = new InputControls();
        }

        private void OnEnable()
        {
            _inputControls.Player.Enable();

            _inputControls.Player.Move.performed += OnMovePerformed;
            _inputControls.Player.Move.canceled += OnMoveCanceled;
        
            // Camera Movement Events
            _inputControls.Player.Look.performed += OnLookPerformed;
            _inputControls.Player.RotateCam.performed += OnRotateCamPerformed;
            _inputControls.Player.Zoom.performed += OnZoomPerformed;

            _inputControls.Player.PauseGame.performed += OnPauseGamePerformed;
        }

        #region Handlers
            private void OnMovePerformed(InputAction.CallbackContext context)
            {
                MoveEvent?.Invoke(context.ReadValue<Vector2>());
            }

            private void OnMoveCanceled(InputAction.CallbackContext context)
            {
                MoveEvent?.Invoke(Vector2.zero);
            }

            private void OnLookPerformed(InputAction.CallbackContext context)
            {
                LookEvent?.Invoke(context.ReadValue<Vector2>());
            }

            private void OnRotateCamPerformed(InputAction.CallbackContext context)
            {
                RotateCamEvent?.Invoke(context.ReadValueAsButton());
            }

            private void OnZoomPerformed(InputAction.CallbackContext context)
            {
                ZoomEvent?.Invoke(context.ReadValue<Vector2>());
            }

            private void OnPauseGamePerformed(InputAction.CallbackContext context)
            {
                PauseGameEvent?.Invoke();
            }
        #endregion

        private void OnDisable()
        {
            _inputControls.Player.Move.performed -= OnMovePerformed;
            _inputControls.Player.Move.canceled -= OnMoveCanceled;
        
            // Camera Movement Events
            _inputControls.Player.Look.performed -= OnLookPerformed;
            _inputControls.Player.RotateCam.performed -= OnRotateCamPerformed;
            _inputControls.Player.Zoom.performed -= OnZoomPerformed;

            _inputControls.Player.PauseGame.performed -= OnPauseGamePerformed;

            _inputControls.Player.Disable();
        }
}
