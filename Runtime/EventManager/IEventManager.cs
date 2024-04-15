using System;

namespace DJM.CoreTools.EventManager
{
    public interface IEventManager
    {
        public void Subscribe<T>(Action<T> listener) where T : struct;
        public void Unsubscribe<T>(Action<T> listener) where T : struct;
        public void TriggerEvent<T>(T eventData) where T : struct;
    }
}