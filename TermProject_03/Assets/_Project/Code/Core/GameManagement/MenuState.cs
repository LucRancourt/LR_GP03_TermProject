using UnityEngine;
using _Project.Code.Core.StateMachine;
using _Project.Code.Core.Events;

namespace _Project.Code.Core.GameManagement
{
    using _Project.Code.Core.ServiceLocator;

    public class MenuState : BaseState
    {
        private readonly GameManagementService _gameManagement;
        private InputController _inputService;

        public MenuState(GameManagementService gameManagement)
        {
            _gameManagement = gameManagement;
        }

        public override void Enter()
        {
            _inputService = ServiceLocator.Get<InputController>();
            Time.timeScale = 1f;
            _inputService?.EnableUIActions();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            ServiceLocator.Get<EventBus>().Publish(new GameStateChangedEvent { StateName = "Menu" });
        }
    }
}
