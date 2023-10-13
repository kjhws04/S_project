using UnityEngine;
using UnityEngine.Events;

namespace TacticsToolkit
{
    public class GameEventGameObjectListener : GameEventListener<GameObject>
    {
        [SerializeField] private GameEventGameObject eventGameObject = null;
        [SerializeField] private UnityEvent<GameObject> response = null;

        public override GameEvent<GameObject> Event => eventGameObject;
        public override UnityEvent<GameObject> Response => response;
    }
}
