using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    //A container that creates all the buttons needed for a characters abilities.
    public class CreateAbilityList : MonoBehaviour
    {
        public GameObject ButtonPrefab;
        public List<GameObject> buttons;

        private Entity activeCharacter;

        private void Start()
        {
            buttons = new List<GameObject>();
        }

        public void SetActiveCharacter(GameObject activeCharacter)
        {
            this.activeCharacter = activeCharacter.GetComponent<Entity>();
        }
        
         
        //When the ability button is clicked, create a new button for every ability the activeCharacter has
        public void CreateCharacterAbilityButtons()
        {
            if (!buttons.Any(x => x.activeInHierarchy))
            {
                foreach (var item in buttons)
                {
                    item.SetActive(false);
                }
                if (activeCharacter && activeCharacter.abilitiesForUse.Count > 0)
                {
                    var abilityList = activeCharacter
                        .abilitiesForUse;

                    foreach (var abilityContainer in abilityList)
                    {
                        var buttonToActivate = buttons.Find(x => !x.activeInHierarchy);

                        if (buttonToActivate == null)
                        {
                            buttonToActivate = Instantiate(ButtonPrefab, transform);
                            buttons.Add(buttonToActivate);
                        }
                        else
                        {
                            buttonToActivate.SetActive(true);
                        }

                        buttonToActivate.transform.GetComponentInChildren<Text>().text = abilityContainer.ability.name;

                        if (abilityContainer.turnsSinceUsed > abilityContainer.ability.cooldown && activeCharacter.GetStat(Stats.CurrentMana).statValue >= abilityContainer.ability.cost)
                        {
                            buttonToActivate.GetComponent<Button>().interactable = true;
                        }
                        else
                        {
                            buttonToActivate.GetComponent<Button>().interactable = false;
                        }
                    }
                }
            }
            else
            {
                ClearAbilityButtons();
            }
        }

        public void ClearAbilityButtons()
        {
            buttons.Clear();
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void DisableAbilityList(string abilityName)
        {
            foreach (var item in buttons)
            {
                item.SetActive(false);
            }
        }
    }
}