using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    //The button click event that tells the AbilityController what to cast
    public class CastAbilityButton : MonoBehaviour
    {
        public GameEventString castAbility;

        public void CastAbilityByName()
        {
            var abilityName = transform.GetComponentInChildren<Text>().text;
            castAbility.Raise(abilityName);
        }
    }
}
