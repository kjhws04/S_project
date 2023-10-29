using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainModelChangeSlot : MonoBehaviour, IPointerClickHandler
{
    MainScene _main;
    UserData _data;

    private void Start()
    {
        _main = FindObjectOfType<MainScene>();
        _data = Managers.Game.GetUserData().GetComponent<UserData>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Stat character = gameObject.GetComponent<Stat>();
            _data._modelImg = character.modelImg;
            _data._backGroundImg = character.backGroundImg;
            _main.ChangeModel(character);
            Managers.UI.CloseAllPopupUI(); 
        }
    }
}
