namespace TacticsToolkit
{
    //Ability containers keep track of when was the last time an ability was used. 
    public class AbilityContainer
    {
        public Ability ability;
        public int turnsSinceUsed;

        public AbilityContainer(Ability ability)
        {
            this.ability = ability;
            turnsSinceUsed = 999;
        }
    }
}
