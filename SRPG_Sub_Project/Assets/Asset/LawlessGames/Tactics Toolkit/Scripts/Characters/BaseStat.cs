using System;
using UnityEngine;

namespace TacticsToolkit
{
    //A stat object used for character attribute scaling on level up. 
    [Serializable]
    public class BaseStat
    {
        [SerializeField]
        public int baseStatValue;

        [SerializeField]
        public AnimationCurve baseStatModifier = new AnimationCurve();
    }
}