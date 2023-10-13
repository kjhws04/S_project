using UnityEngine;
using UnityEngine.Events;

namespace TacticsToolkit
{
    public class GameEventCommandListener : GameEventListener<EventCommand>
    {
        [SerializeField] private GameEventCommand eventGameObject = null;
        [SerializeField] private UnityEvent<EventCommand> response = null;

        public override GameEvent<EventCommand> Event => eventGameObject;
        public override UnityEvent<EventCommand> Response => response;
    }
}
