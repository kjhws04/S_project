using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{
    public CharacterScene _charScene;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Stat stat = GetComponent<Stat>();
            if (stat != null)
            {
                _charScene.ModelInfoChange(stat);
            }
        }
    }
}
