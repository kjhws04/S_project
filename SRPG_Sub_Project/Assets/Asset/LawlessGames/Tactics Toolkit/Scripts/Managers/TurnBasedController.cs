using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TacticsToolkit
{
    public class TurnBasedController : MonoBehaviour
    {
        private List<Entity> teamA = new List<Entity>();
        private List<Entity> teamB = new List<Entity>();

        public TurnSorting turnSorting;

        public GameEventGameObject startNewCharacterTurn;
        public GameEventGameObjectList turnOrderSet;

        public List<Entity> combinedList;

        public bool ignorePlayers = false;
        public bool ignoreEnemies = false;

        public enum TurnSorting
        {
            ConstantAttribute,
            CTB
        };

        void Start()
        {
            if (!ignorePlayers)
                teamA = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<Entity>()).ToList();

            if (!ignoreEnemies)
                teamB = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<Entity>()).ToList();

            combinedList = new List<Entity>();

            foreach (var item in teamA)
            {
                item.teamID = 1;
            }

            foreach (var item in teamB)
            {
                item.teamID = 2;
            }

            SortTeamOrder(true);
        }

        //Sort the team turn order based on TurnSorting.
        private void SortTeamOrder(bool updateListSize = false)
        {
            switch (turnSorting)
            {
                case TurnSorting.ConstantAttribute:
                    if (updateListSize)
                    {
                        combinedList = new List<Entity>();
                        combinedList.AddRange(teamA.Where(x => x.isAlive).ToList());
                        combinedList.AddRange(teamB.Where(x => x.isAlive).ToList());
                        combinedList = combinedList.OrderBy(x => x.statsContainer.Speed.statValue).ToList();
                    }
                    else
                    {
                        Entity item = combinedList[0];
                        combinedList.RemoveAt(0);
                        combinedList.Add(item);
                    }
                    break;
                case TurnSorting.CTB:
                    combinedList = new List<Entity>();
                    combinedList.AddRange(teamA.Where(x => x.isAlive).ToList());
                    combinedList.AddRange(teamB.Where(x => x.isAlive).ToList());
                    combinedList = combinedList.OrderBy(x => x.initiativeValue).ToList();
                    break;
                default:
                    break;
            }

            turnOrderSet.Raise(combinedList.Select(x => x.gameObject).ToList());
        }

        public void StartLevel()
        {
            SortTeamOrder(true);
            if (combinedList.Where(x => x.isAlive).ToList().Count > 0)
            {
                var firsCharacter = combinedList.First();
                firsCharacter.StartTurn();
                startNewCharacterTurn.Raise(firsCharacter.gameObject);
            }
        }

        //On end turn, update the turnorder and start a new characters turn.
        public void EndTurn()
        {
            if (combinedList.Count > 0)
            {
                FinaliseEndCharactersTurn();

                SortTeamOrder();

                foreach (var entity in combinedList)
                    entity.isActive = false;

                if (combinedList.Where(x => x.isAlive).ToList().Count > 0)
                {
                    var firstCharacter = combinedList.First();

                    if (firstCharacter.isAlive)
                    {
                        firstCharacter.isActive = true;
                        firstCharacter.ApplyEffects();

                        if (firstCharacter.isAlive)
                        {
                            firstCharacter.StartTurn();
                            startNewCharacterTurn.Raise(firstCharacter.gameObject);
                        }
                        else
                            EndTurn();


                        foreach (var ability in firstCharacter.abilitiesForUse)
                        {
                            ability.turnsSinceUsed++;
                        }
                    }
                    else
                    {
                        EndTurn();
                    }
                }
            }
        }

        //Last few steps of ending a characters turn. 
        private void FinaliseEndCharactersTurn()
        {
            var characterEndingTurn = combinedList.First();

            if (characterEndingTurn.activeTile && characterEndingTurn.activeTile.tileData)
            {
                //Attach Apply Tile Effect
                var tileEffect = characterEndingTurn.activeTile.tileData.effect;

                if (tileEffect != null)
                    characterEndingTurn.AttachEffect(tileEffect);
            }

            combinedList.First().UpdateInitiative(Constants.BaseCost);
        }


        //Wait until next loop to avoid possible race condition. 
        IEnumerator DelayedSetActiveCharacter(Entity firstCharacter)
        {
            yield return new WaitForFixedUpdate();
            startNewCharacterTurn.Raise(firstCharacter.gameObject);
        }

        //Add a character to the turn order when they spawn. 
        public void SpawnNewCharacter(GameObject character)
        {
            teamA.Add(character.GetComponent<CharacterManager>());
            SortTeamOrder(true);
        }
    }
}
