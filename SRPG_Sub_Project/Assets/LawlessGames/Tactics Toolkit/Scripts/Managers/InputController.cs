using UnityEngine;

namespace TacticsToolkit
{
    public class InputController : MonoBehaviour
    {
        public KeyCode upMovementButton;
        public KeyCode downMovementButton;
        public KeyCode leftMovementButton;
        public KeyCode rightMovementButton;

        public KeyCode actionButton;

        public KeyCode enterMovementModeButton;
        public KeyCode enterAttackModeButton;
        public KeyCode endTurnButton;

        public KeyCode cancelActionButton;

        public GameEvent enterMovementModeEvent;
        public GameEvent enterAttackModeEvent;
        public GameEvent endTurnEvent;
        public GameEvent cancelActionEvent;

        public GameEvent actionButtonPressedEvent;
        public GameEventGameObject focusOnNewTile;

        private OverlayTile activeTile;


        // Update is called once per frame
        private void Update()
        {
            MapBounds bounds = MapManager.Instance.mapBounds;
            MovementActions(bounds);
            GameActions();
        }

        private void GameActions()
        {
            if (Input.GetKeyDown(actionButton))
            {
                actionButtonPressedEvent.Raise();
            }

            if (Input.GetKeyDown(enterMovementModeButton))
            {
                enterMovementModeEvent.Raise();
            }

            if (Input.GetKeyDown(enterAttackModeButton))
            {
                enterAttackModeEvent.Raise();
            }

            if (Input.GetKeyDown(endTurnButton))
            {
                endTurnEvent.Raise();
            }

            if (Input.GetKeyDown(cancelActionButton))
            {
                cancelActionEvent.Raise();
            }
        }

        // record all inputs    

        private void MovementActions(MapBounds bounds)
        {
            if (Input.GetKeyDown(upMovementButton))
            {
                var newGridLocation = new Vector2Int(activeTile.grid2DLocation.x, activeTile.grid2DLocation.y + 1);
                if (newGridLocation.y < bounds.yMax)
                {
                    UpdateActiveTile(newGridLocation);
                }
            }

            if (Input.GetKeyDown(downMovementButton))
            {
                var newGridLocation = new Vector2Int(activeTile.grid2DLocation.x, activeTile.grid2DLocation.y - 1);
                if (newGridLocation.y > bounds.yMin)
                {
                    UpdateActiveTile(newGridLocation);
                }
            }

            if (Input.GetKeyDown(leftMovementButton))
            {
                var newGridLocation = new Vector2Int(activeTile.grid2DLocation.x - 1, activeTile.grid2DLocation.y);
                if (newGridLocation.x >= bounds.xMin)
                {
                    UpdateActiveTile(newGridLocation);
                }
            }

            if (Input.GetKeyDown(rightMovementButton))
            {
                var newGridLocation = new Vector2Int(activeTile.grid2DLocation.x + 1, activeTile.grid2DLocation.y);
                if (newGridLocation.x < bounds.xMax - 1)
                {
                    UpdateActiveTile(newGridLocation);
                }
            }
        }

        private void UpdateActiveTile(Vector2Int newGridLocation)
        {
            var newTile = MapManager.Instance.GetOverlayTileFromGridPosition(newGridLocation);
            focusOnNewTile.Raise(newTile.gameObject);
        }

        public void SetActiveTile(GameObject activeTile) => this.activeTile = activeTile.GetComponent<OverlayTile>();
        
    }
}