using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace TacticsToolkit
{
    public class BattleController : MonoBehaviour
    {
        public Entity activeCharacter;

        public GameEvent clearTiles;
        public GameEventString cancelActionEvent;

        private bool InAttackMode = false;
        private Entity focusedCharacter = null;
        private RangeFinder rangeFinder;

        private bool hasAttacked = false;
        private List<Entity> inRangeCharacters;

        private void Start()
        {
            rangeFinder = new RangeFinder();
        }

        public void SetActiveCharacter(GameObject character)
        {
            activeCharacter = character.GetComponent<Entity>();
        }

        public void ActionButtonPressed()
        {
            if (InAttackMode && focusedCharacter)
            {
                AttackUnit();
            }
        }

        public void CancelActionPressed()
        {
            if (InAttackMode)
            {
                cancelActionEvent.Raise("Attack");
                ResetAttackMode(true);
            }
        }

        //Cancel attack.
        private void ResetAttackMode(bool isAttack = false)
        {
            ResetCharacterFocus();
            hasAttacked = false;
            InAttackMode = false;

            if(isAttack)
                OverlayController.Instance.ClearTiles();
        }

        //Attack targeted entity.
        private void AttackUnit()
        {
            focusedCharacter.TakeDamage(activeCharacter.GetStat(Stats.Strenght).statValue);
            activeCharacter.UpdateInitiative(Constants.AttackCost);
            hasAttacked = true;
            ResetAttackMode(true);
        }

        //Enter attack mode and get all in range characters.
        public void EnterAttackMode()
        {
            if (!hasAttacked && activeCharacter)
            {
                InAttackMode = true;
                var inRangeTiles = rangeFinder.GetTilesInRange(activeCharacter.activeTile, activeCharacter.GetStat(Stats.AttackRange).statValue, true);
                inRangeCharacters = inRangeTiles.Where(x => x.activeCharacter && x.activeCharacter.teamID != activeCharacter.teamID && x.activeCharacter.isAlive).Select(x => x.activeCharacter).ToList();

                if (inRangeCharacters.Count <= 0)
                    InAttackMode = false;
                else
                    DisplayAttackRange();
            }
        }

        public void CheckIfFocusedOnCharacterAndInAttackMode(GameObject focusedOnTile)
        {
            if (InAttackMode)
            {

                OverlayTile tile = focusedOnTile.GetComponent<OverlayTile>();

                if (tile.activeCharacter != null && tile.activeCharacter.teamID != activeCharacter.teamID && tile.activeCharacter.isAlive && inRangeCharacters.Any(x => x == tile.activeCharacter))
                {
                    ResetCharacterFocus();

                    focusedCharacter = tile.activeCharacter;
                    focusedCharacter.SetTargeted(true);
                }
                else
                {
                    ResetCharacterFocus();
                }
            }
        }

        private void ResetCharacterFocus()
        {
            if (focusedCharacter)
            {
                focusedCharacter.SetTargeted(false);
                focusedCharacter = null;
            }
        }

        //Show all the tiles in attack range based on mouse position. 
        public void DisplayAttackRange(GameObject focusedOnTile = null)
        {
            if (activeCharacter)
            {
                var tileToUse = focusedOnTile != null ? focusedOnTile.GetComponent<OverlayTile>() : activeCharacter.activeTile;
                var attackColor = OverlayController.Instance.AttackRangeColor;
                List<OverlayTile> inAttackRangeTiles = rangeFinder.GetTilesInRange(tileToUse, activeCharacter.GetStat(Stats.AttackRange).statValue, true, true);
                OverlayController.Instance.ColorTiles(attackColor, inAttackRangeTiles);
            }
        }

        public void EndTurn() => ResetAttackMode();

        public void HideAttackRange()
        {
            if (!InAttackMode) {
                OverlayController.Instance.ClearTiles(OverlayController.Instance.AttackRangeColor);
            }
        }
    }
}
