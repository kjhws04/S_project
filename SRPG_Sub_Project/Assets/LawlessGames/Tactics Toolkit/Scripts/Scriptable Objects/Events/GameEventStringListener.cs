using UnityEngine;
using UnityEngine.Events;

namespace TacticsToolkit
{
    public class GameEventStringListener : GameEventListener<string>
    {
        [SerializeField] private GameEventString eventGameObject = null;
        [SerializeField] private UnityEvent<string> response = null;

        public override GameEvent<string> Event => eventGameObject;
        public override UnityEvent<string> Response => response;
    }
}
