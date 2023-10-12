using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    //Scipt for showing mana in the UI.
    public class DisplayMana : MonoBehaviour
    {
        private CharacterManager activeCharacter;

        public void UpdateMana(string abilityName)
        {
            var currentMana = this.activeCharacter.GetStat(Stats.CurrentMana).statValue;
            var mana = this.activeCharacter.GetStat(Stats.Mana).statValue;

            GetComponent<Text>().text = currentMana + "/" + mana;
        }

        public void SetCurrentCharactersMana(GameObject activeCharacter)
        {
            this.activeCharacter = activeCharacter.GetComponent<CharacterManager>();
            var currentMana = this.activeCharacter.GetStat(Stats.CurrentMana).statValue;
            var mana = this.activeCharacter.GetStat(Stats.Mana).statValue;


            GetComponent<Text>().text = currentMana + "/" + mana;
        }
    }
}
