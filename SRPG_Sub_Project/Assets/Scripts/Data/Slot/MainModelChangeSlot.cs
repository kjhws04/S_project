using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainModelChangeSlot : MonoBehaviour
{
    MainScene _main;

    private void Start()
    {
        _main = FindObjectOfType<MainScene>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Managers.UI.CloseAllPopupUI();
            Debug.Log("TEST");
        }
    }
}
