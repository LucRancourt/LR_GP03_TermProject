using UnityEngine;

using _Project.Code.Core.Events;
using _Project.Code.Core.StateMachine;


namespace _Project.Code.Core.GameManagement
{
    using _Project.Code.Core.ServiceLocator;

    public class PausedState : BaseState
    {
        private readonly GameManagementService _gameManagement;
        private InputController _inputService;
        private float _previousTimeScale;

        public PausedState(GameManagementService gameManagement)
        {
            _gameManagement = gameManagement;
        }

        public override void Enter()
        {
            _inputService = ServiceLocator.Get<InputController>();
            _previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            _inputService?.EnableUIActions();

            ServiceLocator.Get<EventBus>().Publish(new GameStateChangedEvent { StateName = "Paused" });
            ServiceLocator.Get<EventBus>().Publish(new GamePausedEvent());
        }

        public override void Exit()
        {
            Time.timeScale = _previousTimeScale;
            ServiceLocator.Get<EventBus>().Publish(new GameResumedEvent());
        }
    }
}
