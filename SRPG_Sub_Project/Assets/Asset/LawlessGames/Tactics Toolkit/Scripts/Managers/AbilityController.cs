using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static TacticsToolkit.Ability;

namespace TacticsToolkit
{
    public class AbilityController : MonoBehaviour
    {
        public GameEventString disableAbility;
        public RangeFinder eventRangeController;

        private Entity activeCharacter;
        private List<OverlayTile> abilityRangeTiles;
        private List<OverlayTile> abilityAffectedTiles;
        private ShapeParser shapeParser;
        private AbilityContainer abilityContainer;

        // Start is called before the first frame update
        void Start()
        {
            eventRangeController = new RangeFinder();
            shapeParser = new ShapeParser();
            abilityRangeTiles = new List<OverlayTile>();
            abilityAffectedTiles = new List<OverlayTile>();
        }

        //Cast an ability
        private void CastAbility()
        {
            var inRangeCharacters = new List<Entity>();

            //get in range characters
            foreach (var tile in abilityAffectedTiles)
            {
                var targetCharacter = tile.activeCharacter;
                if (targetCharacter != null && CheckAbilityTargets(abilityContainer.ability.abilityType, targetCharacter) && targetCharacter.isAlive)
                {
                    inRangeCharacters.Add(targetCharacter);
                }
            }

            //attach effects
            foreach (var character in inRangeCharacters)
            {
                foreach (var effect in abilityContainer.ability.effects)
                {
                    character.AttachEffect(effect);
                    if (effect.Duration == 0)
                        character.ApplySingleEffects(effect.GetStatKey());
                }

                //apply value
                switch (abilityContainer.ability.abilityType)
                {
                    case AbilityTypes.Ally:
                        character.HealEntity(abilityContainer.ability.value);
                        break;
                    case AbilityTypes.Enemy:
                        character.TakeDamage(abilityContainer.ability.value);
                        break;
                    case AbilityTypes.All:
                        character.TakeDamage(abilityContainer.ability.value);
                        break;
                    default:
                        break;
                }
            }



            abilityContainer.turnsSinceUsed = 0;
            activeCharacter.UpdateInitiative(Constants.AbilityCost);
            activeCharacter.UpdateMana(abilityContainer.ability.cost);
            disableAbility.Raise(abilityContainer.ability.Name);
            abilityContainer = null;
            OverlayController.Instance.ClearTiles(null);
        }

        //The event receiver for Casting an Ability. 
        public void CastAbilityCommand(EventCommand abilityCommand)
        {
            if (abilityCommand is CastAbilityCommand)
            {
                CastAbilityCommand command = (CastAbilityCommand)abilityCommand;
                CastAbilityParams castAbilityParams = command.StronglyTypedCommandParam();
                abilityAffectedTiles = castAbilityParams.affectedTiles;
                abilityContainer = castAbilityParams.abilityContainer;
                CastAbility();
            }
        }

        //Check if Abilities are targeting the right entities.
        private bool CheckAbilityTargets(AbilityTypes abilityType, Entity characterTarget)
        {
            if (abilityType == AbilityTypes.Enemy)
            {
                return characterTarget.teamID != activeCharacter.teamID;
            }
            else if (abilityType == AbilityTypes.Ally)
            {
                return characterTarget.teamID == activeCharacter.teamID;
            }

            return true;
        }

        public void SetActiveCharacter(GameObject activeChar)
        {
            activeCharacter = activeChar.GetComponent<Entity>();
        }

        //Set the position the abilities origin.
        public void SetAbilityPosition(GameObject focusedOnTile)
        {
            var map = MapManager.Instance.map;
            OverlayTile tilePosition = focusedOnTile.GetComponent<OverlayTile>();
            if (abilityContainer != null)
            {
                foreach (var tile in abilityAffectedTiles)
                {
                    if (map.ContainsKey(tile.grid2DLocation))
                    {
                        var gridTile = map[tile.grid2DLocation];
                        gridTile.GetComponent<SpriteRenderer>().color = abilityRangeTiles.Contains<OverlayTile>(gridTile)
                            ? OverlayController.Instance.MoveRangeColor
                            : new Color(0, 0, 0, 0);
                    }
                }

                if (abilityRangeTiles.Contains(map[tilePosition.grid2DLocation]))
                {
                    abilityAffectedTiles = shapeParser.GetAbilityTileLocations(tilePosition, abilityContainer.ability.abilityShape, activeCharacter.activeTile.grid2DLocation);

                    if (abilityContainer.ability.includeOrigin)
                        abilityAffectedTiles.Add(tilePosition);

                    OverlayController.Instance.ColorTiles(OverlayController.Instance.AttackRangeColor, abilityAffectedTiles);
                }
            }
        }

        //Set ability casting mode. 
        public void AbilityModeEvent(string abilityName)
        {
            OverlayController.Instance.ClearTiles(null);

            var abilityContainer = activeCharacter.abilitiesForUse.Find(x => x.ability.name == abilityName);
            if (abilityContainer.ability.cost <= activeCharacter.statsContainer.CurrentMana.statValue)
            {
                abilityRangeTiles = eventRangeController.GetTilesInRange(activeCharacter.activeTile, abilityContainer.ability.range, true);

                OverlayController.Instance.ColorTiles(OverlayController.Instance.MoveRangeColor, abilityRangeTiles);

                this.abilityContainer = abilityContainer;
            }
        }

        //Cancel ability casting mode. 
        public void CancelEventMode()
        {
            OverlayController.Instance.ClearTiles(null);
            abilityContainer = null;
        }

        public void ActionButtonPressed()
        {
            if(abilityContainer != null)
                CastAbility();
        }
    }
}
