using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    //Displays level information.
    public class LevelReader : MonoBehaviour
    {
        public CharacterManager character;


        public Image expBar;
        public Text expText;

        public GameEvent LevelUp;

        // Start is called before the first frame update
        void Start()
        {
            UpdateText();
        }

        public void LevelChanged()
        {
            UpdateText();
            UpdateExpBar();
        }

        public void ExpUp()
        {
            UpdateText();
            UpdateExpBar();
        }


        public void UpdateExpBar()
        {
            expText.text = character.experience + " / " + character.requiredExperience + " EXP";
            expBar.GetComponent<Image>().fillAmount = (float)character.experience / (float)character.requiredExperience;
        }

        public void UpdateText()
        {
            gameObject.GetComponent<Text>().text = "Level: " + character.level;
            UpdateExpBar();
        }
    }
}
