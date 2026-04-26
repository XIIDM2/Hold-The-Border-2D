using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class EventBus : IEventBus
    {
        private Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

        public void Subscribe<T>(Action<T> method) where T : IEvent
        {
            var eventType = typeof(T);

            if (!_subscribers.TryGetValue(eventType, out List<Delegate> list))
            {
                list = new List<Delegate>();
                _subscribers[eventType] = list;
            }

            list.Add(method);
        }

        public void Unsubscribe<T>(Action<T> method) where T : IEvent
        {
            var eventType = typeof(T);

            if (_subscribers.TryGetValue(eventType, out var list))
            {
                list.Remove(method);

                if (list.Count == 0)
                {
                    _subscribers.Remove(eventType);
                }
            }
        }

        public void Publish<T>(T eventData) where T : IEvent
        {
            var eventType = typeof(T);

            if (_subscribers.TryGetValue(eventType, out var list))
            {
                foreach (Delegate subscriber in list)
                {
                    ((Action<T>)subscriber).Invoke(eventData);
                }
            }
        }
    }
}