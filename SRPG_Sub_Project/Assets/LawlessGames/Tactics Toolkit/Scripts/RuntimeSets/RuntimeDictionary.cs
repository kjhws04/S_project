using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticsToolkit
{
    public class RuntimeDictionary<T, T2> : ScriptableObject
    {
        [SerializeField]
        public Dictionary<T, T2> items = new Dictionary<T, T2>();

        public void Initialize()
        {
            items.Clear();
        }

        public T2 GetValue(T key)
        {
            return items[key];
        }

        public void AddToList(T key, T2 value)
        {
            if (!items.ContainsKey(key))
                items.Add(key, value);
        }

        public void RemoveFromList(T key, T2 value)
        {
            if (items.ContainsKey(key))
                items.Remove(key);
        }
    }
}
