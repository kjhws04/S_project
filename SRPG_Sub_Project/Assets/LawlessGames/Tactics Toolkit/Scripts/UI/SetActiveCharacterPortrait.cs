using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    public class SetActiveCharacterPortrait : MonoBehaviour
    {
        public void SetCharacterImage(GameObject activeCharacter)
        {
            GetComponent<Image>().sprite = activeCharacter.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
