
namespace TacticsToolkit
{
    //The object used for the enemy AI. 
    //A senario is basically an action an enemy can take. 

    public class Senario
    {
        public float senarioValue;
        public AbilityContainer targetAbility;
        public OverlayTile targetTile;
        public OverlayTile positionTile;
        public bool useAutoAttack;

        public Senario(float senarioValue, AbilityContainer targetAbility, OverlayTile targetTile, OverlayTile positionTile, bool useAutoAttack)
        {
            this.senarioValue = senarioValue;
            this.targetAbility = targetAbility;
            this.targetTile = targetTile;
            this.positionTile = positionTile;
            this.useAutoAttack = useAutoAttack;
        }

        public Senario()
        {
            this.senarioValue = -10000;
            this.targetAbility = null;
            this.targetTile = null;
            this.positionTile = null;
            this.useAutoAttack = false;
        }
    }
}