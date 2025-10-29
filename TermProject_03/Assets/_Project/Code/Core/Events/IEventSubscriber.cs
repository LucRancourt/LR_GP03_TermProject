using System;
using System.Collections.Generic;

namespace _Project.Code.Core.Events
{
    public interface IEventSubscriber
    {
        private static readonly Dictionary<IEventSubscriber, List<Action>> _unsubscribeActions = new();

        protected void OnDestroyEventSubscriber()
        {
            if (_unsubscribeActions.TryGetValue(this, out var actionList))
            {
                foreach (var action in actionList)
                    action();

                actionList.Clear();
            }
        }

        protected void Subscribe<T>(Action<T> callback) where T : IEvent
        {
            if (EventBus.Instance)
            {
                EventBus.Instance.Subscribe(this, callback);

                if (!_unsubscribeActions.ContainsKey(this))
                    _unsubscribeActions[this] = new List<Action>();

                _unsubscribeActions[this].Add(() => EventBus.Instance?.Unsubscribe<T>(this));
            }
        }
    }


    /*
        private readonly List<Action> _unsubscribeActions = new();

        protected void Subscribe<T>(Action<T> callback) where T : IEvent
        {
            if (EventBus.Instance)
            {
                EventBus.Instance.Subscribe(this, callback);
                _unsubscribeActions.Add(() => EventBus.Instance?.Unsubscribe<T>(this));
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var unsubscribe in _unsubscribeActions)
            {
                unsubscribe();
            }

            _unsubscribeActions.Clear();
        }
    */
}



