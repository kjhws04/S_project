using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    //Script for displaying the attributes of a character. 
    public class StatReader : MonoBehaviour
    {
        public CharacterManager character;
        public Stats selectedStat;

        private Text text;
        public Text differenceText;
        private int currentValue;
        private int currentLevel;

        // Start is called before the first frame update
        void Start()
        {
            text = gameObject.GetComponent<Text>();
            var stat = character.GetStat(selectedStat);
            currentValue = Mathf.RoundToInt(stat.statValue);
            text.text = selectedStat.ToString() + ": " + stat.statValue;
            currentLevel = character.level;
        }

        public void UpdateText()
        {
            if (character.level != currentLevel)
            {
                currentLevel = character.level;
                var stat = character.GetStat(selectedStat);
                int difference = Mathf.RoundToInt(stat.statValue - currentValue);
                StopAllCoroutines();
                if (difference > 0)
                {
                    differenceText.text = "+" + difference;
                    differenceText.color = new Color(0, 255, 0, 255);
                    StartCoroutine(TickTextUp(difference));
                }
                else
                {
                    differenceText.text = difference.ToString();
                    differenceText.color = new Color(255, 0, 0, 255);
                    StartCoroutine(TickTextDown(difference));
                }
            }
        }

        public IEnumerator TickTextUp(int difference)
        {
            yield return new WaitForSeconds(1f);
            while (difference > 0)
            {
                difference--;
                currentValue++;
                differenceText.text = "+" + difference;
                text.text = selectedStat.ToString() + ": " + currentValue;

                yield return new WaitForSeconds(0.1f);
            }

            var stat = character.GetStat(selectedStat);
            text.text = selectedStat.ToString() + ": " + stat.statValue;
            differenceText.text = "";
        }

        public IEnumerator TickTextDown(int difference)
        {
            yield return new WaitForSeconds(1f);
            while (difference < 0)
            {
                difference++;
                currentValue--;
                differenceText.text = difference.ToString();
                text.text = selectedStat.ToString() + ": " + currentValue;

                yield return new WaitForSeconds(0.1f);
            }

            var stat = character.GetStat(selectedStat);
            text.text = selectedStat.ToString() + ": " + stat.statValue;
            differenceText.text = "";
        }
    }
}
