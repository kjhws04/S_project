using System;
using UnityEngine;

namespace TacticsToolkit
{
    //ScriptableEffects can be attached to both tiles and abilities. 
    [CreateAssetMenu(fileName = "ScriptableEffect", menuName = "ScriptableObjects/ScriptableEffect")]
    public class ScriptableEffect : ScriptableObject
    {
        public Stats statKey;
        public Operation Operator;
        public float Duration;
        public int Value;

        public Stats GetStatKey()
        {
            return statKey;
        }
    }
}
