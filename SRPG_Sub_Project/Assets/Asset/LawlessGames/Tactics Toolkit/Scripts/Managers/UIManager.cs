using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    public class UIManager : MonoBehaviour
    {
        private List<Button> actionButtons;

        // Start is called before the first frame update
        void Start()
        {
            actionButtons = GetComponentsInChildren<Button>().ToList();
        }

        //If it's a character, enable all the UI. If it's an Enemy, disable all the UI.
        public void StartNewCharacterTurn(GameObject activeCharacter)
        {
            if (activeCharacter.GetComponent<Entity>().teamID == 1)
            {
                EnableUI();
            }
            else
            {
                DisableUI();
            }
        }

        //Enable all the buttons.
        public void EnableUI()
        {
            foreach (var item in actionButtons)
            {
                item.interactable = true;
            }
        }

        //Disable all the buttons. 
        public void DisableUI()
        {
            foreach (var item in actionButtons)
            {
                item.interactable = false;
            }
        }

        //Cancel an action and reenable the button. 
        public void CancelActionState(string actionButton)
        {
            var button = actionButtons.Where(x => x.GetComponentInChildren<Text>().text == actionButton).First();
            button.interactable = true;
        }
    }
}
