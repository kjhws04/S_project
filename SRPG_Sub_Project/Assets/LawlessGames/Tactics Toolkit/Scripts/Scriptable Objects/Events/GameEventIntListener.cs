using UnityEngine;
using UnityEngine.Events;

namespace TacticsToolkit
{
    public class GameEventIntListener : GameEventListener<int>
    {
        [SerializeField] private GameEventInt eventGameObject = null;
        [SerializeField] private UnityEvent<int> response = null;

        public override GameEvent<int> Event => eventGameObject;
        public override UnityEvent<int> Response => response;
    }
}
