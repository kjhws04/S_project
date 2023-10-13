using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TacticsToolkit
{
    using static ArrowTranslator;

    public class MovementController : MonoBehaviour
    {
        public float speed;
        public Entity activeCharacter;
        public bool enableAutoMove;
        public bool showAttackRange;
        public bool moveThroughAllies = true;

        public GameEvent endTurnEvent;
        public GameEventGameObject displayAttackRange;
        public GameEventString cancelActionEvent;

        private PathFinder pathFinder;
        private RangeFinder rangeFinder;
        private ArrowTranslator arrowTranslator;
        private List<OverlayTile> path = new List<OverlayTile>();
        private List<OverlayTile> inRangeTiles = new List<OverlayTile>();
        private List<OverlayTile> inAttackRangeTiles = new List<OverlayTile>();
        private OverlayTile focusedTile;
        private bool movementModeEnabled = false;
        private bool isMoving = false;
        private bool hasMoved = false;

        private void Start()
        {
            pathFinder = new PathFinder();
            rangeFinder = new RangeFinder();
            arrowTranslator = new ArrowTranslator();
        }

        // Update is called once per frame
        void Update()
        {
            //Is this the best way? Not sure
            if (activeCharacter && !activeCharacter.isAlive)
            {
                ResetMovementManager();
            }

            if (focusedTile)
            {
                if (inRangeTiles.Contains(focusedTile) && movementModeEnabled && !isMoving && !focusedTile.isBlocked)
                {
                    path = pathFinder.FindPath(activeCharacter.activeTile, focusedTile, inRangeTiles, false, moveThroughAllies);

                    foreach (var item in inRangeTiles)
                    {
                        item.SetArrowSprite(ArrowDirection.None);
                    }

                    for (int i = 0; i < path.Count; i++)
                    {
                        var previousTile = i > 0 ? path[i - 1] : activeCharacter.activeTile;
                        var futureTile = i < path.Count - 1 ? path[i + 1] : null;

                        var arrowDir = arrowTranslator.TranslateDirection(previousTile, path[i], futureTile);
                        path[i].SetArrowSprite(arrowDir);
                    }
                }
            }

            if (path.Count > 0 && isMoving)
            {
                MoveAlongPath();
            }
        }

        public void ActionButtonPressed()
        {
            if (movementModeEnabled && path.Count > 0)
            {
                isMoving = true;
                OverlayController.Instance.ClearTiles(null);
                activeCharacter.UpdateInitiative(Constants.MoveCost);
            }
        }

        public void CancelButtonPressed()
        {
            //Cancel movement
            if (movementModeEnabled)
            {
                cancelActionEvent.Raise("Move");
                ResetMovementManager();
            }
        }


        //Resets movement mode when movement has Finished or is Cancelled. 
        public void ResetMovementManager()
        {
            movementModeEnabled = false;
            isMoving = false; 
            OverlayController.Instance.ClearTiles(null);
            activeCharacter.CharacterMoved();
            path = new List<OverlayTile>();
        }

        //Move along a set path.
        private void MoveAlongPath()
        {
            var step = speed * Time.deltaTime;

            var zIndex = path[0].transform.position.z;
            activeCharacter.transform.position = Vector3.MoveTowards(activeCharacter.transform.position, path[0].transform.position, step);
            //activeCharacter.transform.position = new Vector3(activeCharacter.transform.position.x, activeCharacter.transform.position.y, activeCharacter.transform.position.z);

            if (Vector3.Distance(activeCharacter.transform.position, path[0].transform.position) < 0.0001f)
            {
                //last tile
                if (path.Count == 1)
                    PositionCharacterOnTile(activeCharacter, path[0]);

                path.RemoveAt(0);
            }

            if (path.Count == 0)
            {
                ResetMovementManager();
                hasMoved = true;
                if (enableAutoMove)
                {
                    if (endTurnEvent)
                        endTurnEvent.Raise();
                    else
                        SetActiveCharacter(activeCharacter.gameObject);
                }
            }
        }

        //Get all tiles in movement range. 
        private void GetInRangeTiles()
        {
            var moveColor = OverlayController.Instance.MoveRangeColor;
            if (activeCharacter && activeCharacter.activeTile)
            {
                inRangeTiles = rangeFinder.GetTilesInRange(activeCharacter.activeTile, activeCharacter.GetStat(Stats.MoveRange).statValue, false, moveThroughAllies);
                OverlayController.Instance.ColorTiles(moveColor, inRangeTiles);
            }
        }

        //Link character to tile once movement has finished
        public void PositionCharacterOnTile(Entity character, OverlayTile tile)
        {
            if (tile != null)
            {
                character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
                character.LinkCharacterToTile(tile);
            }
        }

        //Movement event receiver for the AI
        public void MoveCharacterCommand(List<GameObject> pathToFollow)
        {
            if (activeCharacter)
            {
                isMoving = true;
                activeCharacter.UpdateInitiative(Constants.MoveCost);

                if (pathToFollow.Count > 0)
                    path = pathToFollow.Select(x => x.GetComponent<OverlayTile>()).ToList();
            }
        }

        //Moused over new tile and display the attack range. 
        public void FocusedOnNewTile(GameObject focusedOnTile)
        {
            if (!isMoving)
                focusedTile = focusedOnTile.GetComponent<OverlayTile>();

            if (movementModeEnabled && inRangeTiles.Where(x => x.grid2DLocation == focusedTile.grid2DLocation).Any() && !isMoving && showAttackRange && displayAttackRange)
                displayAttackRange.Raise(focusedOnTile);
        }

        //Show all the tiles in attack range based on mouse position. 
        public void ShowAttackRangeTiles(GameObject focusedOnTile)
        {
            var attackColor = OverlayController.Instance.AttackRangeColor;
            inAttackRangeTiles = rangeFinder.GetTilesInRange(focusedOnTile.GetComponent<OverlayTile>(), activeCharacter.GetStat(Stats.AttackRange).statValue, true, moveThroughAllies);

            OverlayController.Instance.ColorTiles(attackColor, inAttackRangeTiles);
        }

        //Set new active character
        public void SetActiveCharacter(GameObject character)
        {
            activeCharacter = character.GetComponent<Entity>();
            hasMoved = false;
            if (enableAutoMove && activeCharacter.isAlive)
                StartCoroutine(DelayedMovementmode());
        }

        //Wait until next loop to avoid possible race condition. 
        IEnumerator DelayedMovementmode()
        {
            yield return new WaitForFixedUpdate();
            EnterMovementMode();
        }

        //Set a character to a tile when it spawns. 
        public void SpawnCharacter(GameObject newCharacter)
        {
            PositionCharacterOnTile(newCharacter.GetComponent<Entity>(), focusedTile);
        }

       

        //Enter movement mode on button click.
        public void EnterMovementMode()
        {
            if (!hasMoved)
            {
                GetInRangeTiles();
                movementModeEnabled = true;
            }
        }
    }
}
