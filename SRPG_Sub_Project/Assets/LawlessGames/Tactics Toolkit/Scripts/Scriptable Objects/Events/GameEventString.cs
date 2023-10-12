using System;
using UnityEngine;

namespace TacticsToolkit
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameEventString", menuName = "GameEvents/GameEventString", order = 2)]
    public class GameEventString : GameEvent<string>
    {
        public string _string;
    }
}