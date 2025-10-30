using UnityEngine;

using _Project.Code.Core.Events;


namespace _Project.Code.Gameplay.Input
{
    public struct MoveInputEvent : IEvent
    {
        public Vector2 Input;
    }

    public struct LookInputEvent : IEvent
    {
        public Vector2 Input;
    }

    public struct InteractInputEvent : IEvent { }

    public struct CameraRotateInputEvent : IEvent
    {
        public bool IsRotating;
    }

    public struct ZoomInputEvent : IEvent
    {
        public float Input;
    }

    public struct PauseInputEvent : IEvent { }
}