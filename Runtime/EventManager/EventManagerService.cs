using System;
using System.Collections.Generic;
using UnityEngine;

namespace DJM.CoreTools.EventManager
{
    public class EventManagerService : IEventManager
    {
        private const string NullListenerErrorMessage = "Attempting to subscribe a null listener to event";
        private const string ExceptionOnEventListerTrigger = "Exception caught when triggering event listener - {0}";
        
        private readonly Dictionary<Type, Delegate> _eventTable = new();

        public void Subscribe<T>(Action<T> listener) where T : struct
        {
            var eventId = typeof(T);
            
            if (listener is null)
            {
                LogError(eventId, NullListenerErrorMessage);
                return;
            }

            if (_eventTable.TryAdd(eventId, listener)) return;
            _eventTable[eventId] = Delegate.Combine(_eventTable[eventId], listener);
        }
        
        public void Unsubscribe<T>(Action<T> listener) where T : struct
        {
            var eventId = typeof(T);
            if (!_eventTable.ContainsKey(eventId)) return;
            _eventTable[eventId] = Delegate.Remove(_eventTable[eventId], listener);
            
            if (_eventTable[eventId] is null) _eventTable.Remove(eventId);
        }
        
        public void TriggerEvent<T>(T eventData) where T : struct
        {
            var eventId = typeof(T);
            if (!_eventTable.TryGetValue(eventId, out var value)) return;
            
            foreach (var listener in value.GetInvocationList())
            {
                try
                {
                    ((Action<T>)listener)?.Invoke(eventData);
                }
                catch (Exception exception)
                {
                    LogError(eventId, string.Format(ExceptionOnEventListerTrigger, exception.Message));
                }
            }
        }
        
        public void ClearEvent<T>() where T : struct => _eventTable.Remove(typeof(T));
        public void Reset() => _eventTable.Clear();

        private static void LogError(Type eventId, string message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError($"{nameof(EventManager)}: {message} ({eventId})");
#endif
        }
    }
}