using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TacticsToolkit
{
    //Haven't found a great use for this yet. A list of scriptable objects. 
    public class RuntimeSet<T> : ScriptableObject
    {
        public List<T> items = new List<T>();

        public void Initialize()
        {
            items.Clear();
        }

        public T GetItemByIndex(int index)
        {
            return items[index];
        }

        public void AddToList(T itemToAdd)
        {
            if (!items.Contains(itemToAdd))
                items.Add(itemToAdd);
        }

        public void RemoveFromList(T itemToRemove)
        {
            if (items.Contains(itemToRemove))
                items.Remove(itemToRemove);
        }
    }
}
