using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TacticsToolkit
{
    public class GamepadInputController : MonoBehaviour
    {
        // Map all controller inputs
        public KeyCode[] buttonNames;
        public string[] axisNames;

        public KeyCode actionButtonKeyCode = KeyCode.Joystick1Button1;
        public KeyCode cancelButtonKeyCode = KeyCode.Joystick1Button2;
        public KeyCode endTurnButtonKeyCode = KeyCode.Joystick1Button3;
        public KeyCode attackModeButtonKeyCode = KeyCode.Joystick1Button4;
        public KeyCode moveModeButtonKeyCode = KeyCode.Joystick1Button5;


        public GameEvent enterMovementModeEvent;
        public GameEvent enterAttackModeEvent;
        public GameEvent endTurnEvent;
        public GameEvent cancelActionEvent;

        public GameEvent actionButtonPressedEvent;
        public GameEventGameObject focusOnNewTile;

        private OverlayTile activeTile;


        void Start()
        {
            // Initialize buttonNames array
            buttonNames = new KeyCode[28];
            buttonNames[0] = KeyCode.JoystickButton0;
            buttonNames[1] = KeyCode.JoystickButton1;
            buttonNames[2] = KeyCode.JoystickButton2;
            buttonNames[3] = KeyCode.JoystickButton3;
            buttonNames[4] = KeyCode.JoystickButton4;
            buttonNames[5] = KeyCode.JoystickButton5;
            buttonNames[6] = KeyCode.JoystickButton6;
            buttonNames[7] = KeyCode.JoystickButton7;
            buttonNames[8] = KeyCode.JoystickButton8;
            buttonNames[9] = KeyCode.JoystickButton9;
            buttonNames[10] = KeyCode.JoystickButton10;
            buttonNames[11] = KeyCode.JoystickButton11;
            buttonNames[12] = KeyCode.JoystickButton12;
            buttonNames[13] = KeyCode.JoystickButton13;
            buttonNames[14] = KeyCode.JoystickButton14;
            buttonNames[15] = KeyCode.JoystickButton15;
            buttonNames[16] = KeyCode.JoystickButton16;
            buttonNames[17] = KeyCode.JoystickButton17;
            buttonNames[18] = KeyCode.JoystickButton18;
            buttonNames[19] = KeyCode.JoystickButton19;
        }

        void Update()
        {
            // Read all button inputs
            for (int i = 0; i < buttonNames.Length; i++)
            {
                if (Input.GetKeyDown(buttonNames[i]))
                {
                    Debug.Log(buttonNames[i] + " button down");
                }
                if (Input.GetKeyUp(buttonNames[i]))
                {
                    Debug.Log(buttonNames[i] + " button up");
                }
            }

            MovementInput();

            if (Input.GetKeyDown(moveModeButtonKeyCode))
            {
                enterMovementModeEvent.Raise();
            }
            if (Input.GetKeyDown(attackModeButtonKeyCode))
            {
                enterAttackModeEvent.Raise();
            }
            if (Input.GetKeyDown(cancelButtonKeyCode))
            {
                cancelActionEvent.Raise();
            }
            if (Input.GetKeyDown(endTurnButtonKeyCode))
            {
                endTurnEvent.Raise();
            }

            if (Input.GetKeyDown(actionButtonKeyCode))
            {
                actionButtonPressedEvent.Raise();
            }
        }

        private void MovementInput()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            MapBounds bounds = MapManager.Instance.mapBounds;
            if (verticalInput >= 1f)
            {
                var newGridLocation = new Vector2Int(activeTile.grid2DLocation.x, activeTile.grid2DLocation.y + 1);
                if (newGridLocation.y < bounds.yMax)
                {
                    StartCoroutine(UpdateActiveTile(newGridLocation));
                }
            }

            if (verticalInput <= -1f)
            {
                var newGridLocation = new Vector2Int(activeTile.grid2DLocation.x, activeTile.grid2DLocation.y - 1);
                if (newGridLocation.y > bounds.yMin)
                {
                    StartCoroutine(UpdateActiveTile(newGridLocation));
                }
            }

            if (horizontalInput <= -1f)
            {
                var newGridLocation = new Vector2Int(activeTile.grid2DLocation.x - 1, activeTile.grid2DLocation.y);
                if (newGridLocation.x >= bounds.xMin)
                {
                    StartCoroutine(UpdateActiveTile(newGridLocation));
                }
            }

            if (horizontalInput >= 1f)
            {
                var newGridLocation = new Vector2Int(activeTile.grid2DLocation.x + 1, activeTile.grid2DLocation.y);
                if (newGridLocation.x < bounds.xMax - 1)
                {
                    StartCoroutine(UpdateActiveTile(newGridLocation));
                }
            }
        }

        private IEnumerator UpdateActiveTile(Vector2Int newGridLocation)
        {
            yield return new WaitForSeconds(0.1f);
            var newTile = MapManager.Instance.GetOverlayTileFromGridPosition(newGridLocation);
            focusOnNewTile.Raise(newTile.gameObject);
        }


        public void SetActiveTile(GameObject activeTile) => this.activeTile = activeTile.GetComponent<OverlayTile>();
    }
}
