using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{
    public CharacterScene _charScene;
    UserData userData;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            userData = Managers.Game.GetUserData().GetComponent<UserData>();
            Stat stat = GetComponent<Stat>();

            _charScene.ModelInfoChange(Util.GetStatData(userData._userCharData , stat.Name));
        }
    }
}
