using Infrastructure.Interfaces;
using System;

namespace Infrastructure.Services
{
    public interface IEventBus
    {
        public void Subscribe<T>(Action<T> method) where T : struct, IEvent;
        public void Unsubscribe<T>(Action<T> method) where T : struct, IEvent;
        public void Publish<T>(T eventData) where T : struct, IEvent;
    }
}