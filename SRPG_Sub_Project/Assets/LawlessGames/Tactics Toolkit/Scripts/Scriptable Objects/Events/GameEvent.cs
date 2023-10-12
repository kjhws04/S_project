// ----------------------------------------------------------------------------
// Based on Ryan Hipple's (Schell Games) talk
// @ Unite 2017 - Game Architecture with Scriptable Objects
// https://github.com/roboryantron/Unite2017 (MIT)
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TacticsToolkit
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvents/GameEvent", order = 1)]
    public class GameEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<GameEventListenerInternal> eventListeners =
            new List<GameEventListenerInternal>();

        public void Raise()
        {
            Raise(null);
        }

        public void Raise(Action onRaised)
        {
            if (name != "CorsorEvent" && name != "FocusedOnNewTile")
                Debug.Log(this.name);

            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
            onRaised?.Invoke();
        }

        public void RegisterListener(GameEventListenerInternal listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListenerInternal listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }

    [Serializable]
    public class GameEvent<T> : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<GameEventListenerInternal<T>> eventListeners =
            new List<GameEventListenerInternal<T>>();

        public virtual void Raise(T param)
        {
            Raise(param, null);
        }

        public virtual void Raise(T param, Action onRaised)
        {
            if (name != "FocusOnTile")
                Debug.Log(this.name);

            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(param);
            onRaised?.Invoke();
        }

        public void RegisterListener(GameEventListenerInternal<T> listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListenerInternal<T> listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}