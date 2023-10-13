// ----------------------------------------------------------------------------
// Based on Ryan Hipple's (Schell Games) talk
// @ Unite 2017 - Game Architecture with Scriptable Objects
// https://github.com/roboryantron/Unite2017 (MIT)
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;

namespace TacticsToolkit
{
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        private GameEventListenerInternal listener = new GameEventListenerInternal();

        private void OnEnable()
        {
            if (Event != null)
            {
                listener.RegisterEvent(Event, Response);
            }
        }

        private void OnDisable()
        {
            if (Event != null)
            {
                listener.UnregisterEvent();
            }
        }
    }

    public abstract class GameEventListener<T> : MonoBehaviour
    {
        public abstract GameEvent<T> Event { get; }
        public abstract UnityEvent<T> Response { get; }

        private GameEventListenerInternal<T> listener = new GameEventListenerInternal<T>();

        private void OnEnable()
        {
            if (Event != null)
            {
                listener.RegisterEvent(Event, Response);
            }
        }

        private void OnDisable()
        {
            if (Event != null)
            {
                listener.UnregisterEvent();
            }
        }
    }
}