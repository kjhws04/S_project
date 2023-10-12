using System;
using UnityEngine;

namespace TacticsToolkit
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameEventGameObject", menuName = "GameEvents/GameEventGameObject", order = 2)]
    public class GameEventGameObject : GameEvent<GameObject>
    {
        public GameObject gameObject;
    }
}
