using System;
using UnityEngine;

namespace TacticsToolkit
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameEventCommand", menuName = "GameEvents/GameEventCommand", order = 3)]
    public class GameEventCommand : GameEvent<EventCommand>
    {
        public EventCommand value;
    }
}
