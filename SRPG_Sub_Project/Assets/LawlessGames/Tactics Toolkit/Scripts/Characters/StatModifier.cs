
namespace TacticsToolkit
{
    //A modifier is an effect that's been attached to a attribute. Could be a buff or a debuff.
    public class StatModifier
    {
        public Stats attributeName;
        public int value;
        public float duration;
        public Operation Operator;
        public bool isActive;
        public string statModName;

        public StatModifier(Stats attribute, int value, float duration, Operation op, string statModName)
        {
            this.attributeName = attribute;
            this.value = value;
            this.duration = duration;
            this.Operator = op;
            this.statModName = statModName;
            isActive = true;
        }
    }
}
