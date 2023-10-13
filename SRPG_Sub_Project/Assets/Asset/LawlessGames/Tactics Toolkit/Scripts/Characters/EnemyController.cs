using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace TacticsToolkit
{
    public class EnemyController : Entity
    {
        //How the enemy will act
        public Personality personality = Personality.Strategic;

        //List of players in the game
        public List<CharacterManager> playerCharacters;

        //Events that enemys can raise
        public GameEventGameObjectList moveAlongPath;
        public GameEventCommand castAbility;
        public GameEventString logAction;

        private List<EnemyController> enemyCharacters;
        private List<OverlayTile> path;
        private ShapeParser shapeParser;
        private Senario bestSenario;
        private RangeFinder rangeFinder;
        private PathFinder pathFinder;

        public enum Personality
        {
            Aggressive,
            Defensive,
            Strategic
        }

        // Start is called before the first frame update
        public void Start()
        {
            //getTeams
            playerCharacters = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<CharacterManager>()).ToList();
            enemyCharacters = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<EnemyController>()).ToList();
            pathFinder = new PathFinder();
            rangeFinder = new RangeFinder();
            shapeParser = new ShapeParser();
        }

        new void Update()
        {
            base.Update();
        }

        public override void StartTurn()
        {
            StartCoroutine(CalculateBestSenario());
        }

        public override void CharacterMoved()
        {
            logAction.Raise(gameObject.name + ": Moved To " + bestSenario.positionTile.grid2DLocation);

            //Once a character has finished moving, check if a attack/ability is available and do it. Otherwise, end turn
            if (bestSenario != null && (bestSenario.targetTile != null || bestSenario.targetAbility != null))
                Attack();
            else
                StartCoroutine(EndTurn());
        }

        private Senario AutoAttackBasedOnPersonality(OverlayTile position)
        {
            switch (personality)
            {
                case Personality.Aggressive:
                    //Aggressive should attack the closest character while getting as close as possible. 
                    return AggressiveBasicAttackTarget(position);
                case Personality.Defensive:
                    //Defensive should attack the closest character while maintaining the maximum distance. 
                    return DefenciveBasicAttackTarget(position);
                case Personality.Strategic:
                    //Strategic Should attack the lowest health character while maintaining the maximum distance. 
                    return StrategicBasicAttackTarget(position);
                default:
                    break;
            }

            return new Senario();
        }

        //Target Lowest Health Character
        private Senario StrategicBasicAttackTarget(OverlayTile position)
        {
            var targetCharacter = FindClosestToDeathCharacter(position);
            if (targetCharacter)
            {
                var closestDistance = pathFinder.GetManhattenDistance(position, targetCharacter.activeTile);

                if (closestDistance <= GetStat(Stats.AttackRange).statValue)
                {
                    //calculate senarioValue;
                    var senarioValue = GetStat(Stats.Strenght).statValue
                        + closestDistance
                        - targetCharacter.GetStat(Stats.CurrentHealth).statValue;

                    //we can kill
                    if (targetCharacter.GetStat(Stats.CurrentHealth).statValue < GetStat(Stats.Strenght).statValue)
                    {
                        senarioValue = 10000;
                    }

                    return new Senario(senarioValue, null, targetCharacter.activeTile, position, true);
                }
            }

            return new Senario();
        }

        private Senario DefenciveBasicAttackTarget(OverlayTile position)
        {
            var targetCharacter = FindClosestCharacter(position);

            if (targetCharacter)
            {
                var closestDistance = pathFinder.GetManhattenDistance(position, targetCharacter.activeTile);

                //Check if the closest character is in attack range
                if (closestDistance <= GetStat(Stats.AttackRange).statValue)
                {
                    //calculate senarioValue;
                    var senarioValue = 0;
                    senarioValue += GetStat(Stats.Strenght).statValue + (closestDistance - GetStat(Stats.MoveRange).statValue);
                    return new Senario(senarioValue, null, targetCharacter.activeTile, position, true);
                }
            }
            return new Senario();
        }

        private Senario AggressiveBasicAttackTarget(OverlayTile position)
        {
            var targetCharacter = FindClosestCharacter(position);

            if (targetCharacter)
            {
                var closestDistance = pathFinder.GetManhattenDistance(position, targetCharacter.activeTile);

                //Check if the closest character is in attack range and make sure we're not on the characters tile. 
                if (closestDistance <= GetStat(Stats.AttackRange).statValue && position != targetCharacter)
                {
                    var targetTile = GetClosestNeighbour(targetCharacter.activeTile);

                    //calculate senarioValue;
                    var senarioValue = 0;
                    senarioValue += GetStat(Stats.Strenght).statValue + (GetStat(Stats.MoveRange).statValue - closestDistance);

                    return new Senario(senarioValue, null, targetCharacter.activeTile, position, true);
                }
            }

            return new Senario();
        }

        //Get the closest character
        private Entity FindClosestCharacter(OverlayTile position)
        {
            Entity targetCharacter = null;

            var closestDistance = 1000;
            foreach (var player in playerCharacters)
            {
                if (player.isAlive)
                {
                    var currentDistance = pathFinder.GetManhattenDistance(position, player.activeTile);

                    if (currentDistance <= closestDistance)
                    {
                        closestDistance = currentDistance;
                        targetCharacter = player;
                    }
                }
            }

            return targetCharacter;
        }

        //Should move for the lowest inRange Health Character
        private Entity FindClosestToDeathCharacter(OverlayTile position)
        {
            Entity targetCharacter = null;
            int lowestHealth = -1;
            var noCharacterInRange = true;
            foreach (var player in playerCharacters)
            {
                if (player.isAlive && player.activeTile)
                {
                    var currentDistance = pathFinder.GetManhattenDistance(position, player.activeTile);
                    var currentHealth = player.GetStat(Stats.CurrentHealth).statValue;

                    if (currentDistance <= GetStat(Stats.AttackRange).statValue &&
                        ((lowestHealth == -1) || (currentHealth <= lowestHealth || noCharacterInRange)))
                    {
                        lowestHealth = currentHealth;
                        targetCharacter = player;
                        noCharacterInRange = false;
                    }
                    else if (noCharacterInRange && ((lowestHealth == -1) || (currentHealth <= lowestHealth)))
                    {
                        lowestHealth = currentHealth;
                        targetCharacter = player;
                    }
                }
            }

            //can't travel to units tile so get the closest neighbour
            return targetCharacter;
        }

        //Finds the closest tile that's next to a tile.  
        private OverlayTile GetClosestNeighbour(OverlayTile targetCharacterTile)
        {
            var targetNeighbours = MapManager.Instance.GetNeighbourTiles(targetCharacterTile, new List<OverlayTile>());
            var targetTile = targetNeighbours[0];
            var targetDistance = pathFinder.GetManhattenDistance(targetTile, activeTile);

            foreach (var item in targetNeighbours)
            {
                var distance = pathFinder.GetManhattenDistance(item, activeTile);

                if (distance < targetDistance)
                {
                    targetTile = item;
                    targetDistance = distance;
                }
            }

            return targetTile;
        }

        //Find all the characters within a group of tiles. 
        private List<Entity> FindAllCharactersInTiles(List<OverlayTile> tiles)
        {
            var playersInRange = new List<Entity>();
            foreach (var tile in tiles)
            {
                //Need to change this to account for friendly abilitys
                if (tile.activeCharacter && tile.activeCharacter.teamID != teamID && tile.activeCharacter.isAlive)
                {
                    playersInRange.Add(tile.activeCharacter);
                }
            }

            return playersInRange;
        }

        private void Attack()
        {
            //If we can attack, we attack, if we have an ability, cast the ability
            if (bestSenario.useAutoAttack && bestSenario.targetTile.activeCharacter)
                StartCoroutine(AttackTargettedCharacter(bestSenario.targetTile.activeCharacter));
            else if (bestSenario.targetAbility != null)
                StartCoroutine(CastAbility());
        }

        private IEnumerator CastAbility()
        {
            var abilityAffectedTiles = shapeParser.GetAbilityTileLocations(bestSenario.targetTile, bestSenario.targetAbility.ability.abilityShape, bestSenario.positionTile.grid2DLocation);
            abilityAffectedTiles.Add(bestSenario.targetTile);
            OverlayController.Instance.ColorTiles(OverlayController.Instance.AttackRangeColor, abilityAffectedTiles);
            yield return new WaitForSeconds(0.5f);

            //Tell the AbilityController to cast this ability
            CastAbilityParams abilityCommandParams = new CastAbilityParams(abilityAffectedTiles, bestSenario.targetAbility);
            CastAbilityCommand abilityCommand = new CastAbilityCommand();
            abilityCommand.CommandParams = abilityCommandParams;
            castAbility.Raise(abilityCommand);

            logAction.Raise(gameObject.name + ": Using " + bestSenario.targetAbility.ability.name);
            StartCoroutine(EndTurn());
        }

        //Attack target
        private IEnumerator AttackTargettedCharacter(Entity targetedCharacter)
        {
            OverlayController.Instance.ColorSingleTile(OverlayController.Instance.BlockedTileColor, targetedCharacter.activeTile);

            yield return new WaitForSeconds(0.5f);

            logAction.Raise(gameObject.name + ": " + GetStat(Stats.Strenght).statValue + " Damage");

            //As an example, damage is just the strenght stat. 
            targetedCharacter.TakeDamage(GetStat(Stats.Strenght).statValue);
            UpdateInitiative(Constants.AttackCost);
            StartCoroutine(EndTurn());
        }

        private IEnumerator EndTurn()
        {
            yield return new WaitForSeconds(0.25f);
            OverlayController.Instance.ClearTiles();
            logAction.Raise(gameObject.name + ": End Turn");

            endTurn.Raise();
        }

        //Need to find best senario
        private IEnumerator CalculateBestSenario()
        {
            var tileInMovementRange = rangeFinder.GetTilesInRange(activeTile, GetStat(Stats.MoveRange).statValue);
            OverlayController.Instance.ColorTiles(OverlayController.Instance.MoveRangeColor, tileInMovementRange);
            var senario = new Senario();
            foreach (var tile in tileInMovementRange)
            {
                if(tile.grid2DLocation == new Vector2Int(-4, -1) || tile.grid2DLocation == new Vector2Int(-3, -3))
                {
                    Debug.Log("Test");
                }

                if (!tile.isBlocked)
                {
                    var tempSenario = CreateTileSenarioValue(tile);
                    ApplyTileEffectsToSenarioValue(tile, tempSenario);
                    senario = CompareSenarios(senario, tempSenario);
                    senario = CheckIfSenarioValuesAreEqual(tileInMovementRange, senario, tempSenario);
                    senario = CheckSenarioValueIfNoTarget(senario, tile, tempSenario);
                }
            }


            if (senario.positionTile)
            {
                ApplyBestSenario(senario);
            }
            else
            {
                StartCoroutine(EndTurn());
            }

            yield return null;
        }

        //Execute the best senario
        private void ApplyBestSenario(Senario senario)
        {
            bestSenario = senario;
            var currentTile = activeTile;
            path = pathFinder.FindPath(currentTile, bestSenario.positionTile, new List<OverlayTile>());

            //If it can attack but it doesn't need to move, attack
            if (path.Count == 0 && bestSenario.targetTile != null)
            {
                Attack();
            }
            else
            {
                //we're moving
                StartCoroutine(Move(path));
            }
        }
        //Apply the tile effects to the Senario Value. i.e. Burn Damage from lava tiles.
        private void ApplyTileEffectsToSenarioValue(OverlayTile tile, Senario tempSenario)
        {
            if (tile.tileData && tile.tileData.effect)
            {
                var tileEffectValue = GetEffectsSenarioValue(new List<ScriptableEffect>() { tile.tileData.effect }, new List<Entity>() { this });
                tempSenario.senarioValue -= tileEffectValue;
            }
        }

        //Check is the new senario better than the current best senario.
        private static Senario CompareSenarios(Senario senario, Senario tempSenario)
        {
            if ((tempSenario != null && tempSenario.senarioValue > senario.senarioValue))
            {
                senario = tempSenario;
            }

            return senario;
        }

        //if the new senario and the current best senario are equal, then take the closest senario. 
        private Senario CheckIfSenarioValuesAreEqual(List<OverlayTile> tileInMovementRange, Senario senario, Senario tempSenario)
        {
            if (tempSenario.positionTile != null && tempSenario.senarioValue == senario.senarioValue)
            {
                var tempSenarioPathCount = pathFinder.FindPath(activeTile, tempSenario.positionTile, tileInMovementRange).Count;
                var senarioPathCount = pathFinder.FindPath(activeTile, senario.positionTile, tileInMovementRange).Count;

                if (tempSenarioPathCount < senarioPathCount)
                    senario = tempSenario;
            }

            return senario;
        }

        //If we have no attack target, check how close the tile is to a character.
        private Senario CheckSenarioValueIfNoTarget(Senario senario, OverlayTile tile, Senario tempSenario)
        {
            if (tempSenario.positionTile == null && !senario.targetTile)
            {
                var targetCharacter = FindClosestToDeathCharacter(tile);
                if (targetCharacter)
                {
                    var targetTile = GetClosestNeighbour(targetCharacter.activeTile);

                    if (targetCharacter && targetTile)
                    {
                        var pathToCharacter = pathFinder.FindPath(tile, targetTile, new List<OverlayTile>());
                        var distance = pathToCharacter.Count;

                        var senarioValue = -distance - targetCharacter.GetStat(Stats.CurrentHealth).statValue;
                        if (distance >= GetStat(Stats.AttackRange).statValue)
                        {
                            if (tile.tileData && tile.tileData.effect)
                            {
                                var tileEffectValue = GetEffectsSenarioValue(new List<ScriptableEffect>() { tile.tileData.effect }, new List<Entity>() { this });
                                senarioValue -= tileEffectValue;
                            }

                            if (tile.grid2DLocation != activeTile.grid2DLocation && tile.grid2DLocation != targetCharacter.activeTile.grid2DLocation && (senarioValue > senario.senarioValue || !senario.positionTile))
                                senario = new Senario(senarioValue, null, null, tile, false);
                        }
                    }
                }
            }

            return senario;
        }

        //Tell the MovementController to move the enemy along a path. 
        private IEnumerator Move(List<OverlayTile> path)
        {
            OverlayController.Instance.ColorSingleTile(OverlayController.Instance.MoveRangeColor, bestSenario.positionTile);
            yield return new WaitForSeconds(0.25f);
            moveAlongPath.Raise(path.Select(x => x.gameObject).ToList());
        }


        //Create a senario based on if the enemy can attack from this tile. 
        private Senario CreateTileSenarioValue(OverlayTile overlayTile)
        {
            //Basic Attack
            var attackSenario = AutoAttackBasedOnPersonality(overlayTile);

            foreach (var abilityContainer in abilitiesForUse)
            {
                if (GetStat(Stats.CurrentMana).statValue >= abilityContainer.ability.cost && abilityContainer.turnsSinceUsed >= abilityContainer.ability.cooldown)
                {
                    //Abilities
                    var tempSenario = CreateAbilitySenario(abilityContainer, overlayTile);

                    if (tempSenario.senarioValue > attackSenario.senarioValue)
                        attackSenario = tempSenario;
                }
            }
            return attackSenario;
        }

        //Calculate the senario value of an ability. 
        private Senario CreateAbilitySenario(AbilityContainer abilityContainer, OverlayTile position)
        {
            var tilesInAbilityRange = rangeFinder.GetTilesInRange(position, abilityContainer.ability.range, true);
            var senario = new Senario();
            foreach (var tile in tilesInAbilityRange)
            {
                var abilityAffectedTiles = shapeParser.GetAbilityTileLocations(tile, abilityContainer.ability.abilityShape, position.grid2DLocation);

                //How many players can the ability hit
                var players = FindAllCharactersInTiles(abilityAffectedTiles);

                var totalAbilityDamage = GetEffectsSenarioValue(abilityContainer.ability.effects, players);

                if (players.Count > 0)
                {
                    var totalPlayerHealth = 0;
                    var weakestPlayerHealth = int.MaxValue;
                    var closestDistance = 0;
                    var damageValue = 0;
                    foreach (var player in players)
                    {
                        closestDistance = -1000;
                        totalPlayerHealth += GetStat(Stats.CurrentHealth).statValue;

                        if (GetStat(Stats.CurrentHealth).statValue < weakestPlayerHealth)
                            weakestPlayerHealth = GetStat(Stats.CurrentHealth).statValue;

                        var tempClosestDistance = pathFinder.GetManhattenDistance(position, player.activeTile);

                        if (tempClosestDistance > closestDistance)
                            closestDistance = tempClosestDistance;

                        totalAbilityDamage += abilityContainer.ability.value;
                    }

                    damageValue += totalAbilityDamage;

                    var tempSenarioValue = damageValue
                            + closestDistance
                            - weakestPlayerHealth;

                    if (tempSenarioValue > senario.senarioValue)
                    {
                        senario = new Senario(tempSenarioValue, abilityContainer, tile, position, false);
                    }
                }
            }

            return senario;
        }

        //Calculate how much an effect will influence the Senario Value
        private static int GetEffectsSenarioValue(List<ScriptableEffect> effectsContainer, List<Entity> entities)
        {
            var totalDamage = 0;
            int totalSenarioValue = 0;

            if (effectsContainer.Count > 0 && entities.Count > 0)
            {
                foreach (var effect in effectsContainer)
                {
                    foreach (var entity in entities)
                    {
                        if (effect.Operator == Operation.Minus)
                        {
                            totalDamage += Mathf.RoundToInt(effect.Value * (effect.Duration > 0 ? effect.Duration : 1));
                        }
                        else if (effect.Operator == Operation.MinusByPercentage)
                        {
                            var value = entity.GetStat(Stats.CurrentHealth).statValue / 100 * effect.Value;
                            totalDamage += Mathf.RoundToInt(value * (effect.Duration > 0 ? effect.Duration : 1));
                        }


                        if (totalDamage >= entity.GetStat(Stats.CurrentHealth).statValue)
                            totalSenarioValue += 10000;
                        else
                            totalSenarioValue += totalDamage;
                    }
                }
            }

            return totalSenarioValue;
        }
    }
}
