using UnityEngine;

namespace TacticsToolkit
{
    //Generate character stats with the character class and levels.
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats", order = 1)]
    public class CharacterStats : ScriptableObject
    {
        public Stat Health;
        public Stat Mana;
        public Stat Strenght;
        public Stat Endurance;
        public Stat Speed;
        public Stat Intelligence;
        public Stat MoveRange;
        public Stat AttackRange;
        public Stat CurrentHealth;
        public Stat CurrentMana;


        public Stat getStat(Stats statKey)
        {
            var fields = typeof(CharacterStats).GetFields();

            foreach (var item in fields)
            {
                var type = item.FieldType;
                Stat value = (Stat)item.GetValue(this);

                if (value.statKey == statKey)
                    return value;
            }

            return null;
        }
    }
}
