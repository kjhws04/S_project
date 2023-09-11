using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Recall_Show_Popup : UI_Popup
{
    public bool isShowing = true;
    public int recallTime = 0;

    public override void Init()
    {
        base.Init();
        isShowing = true;

        #region Bind
        #endregion
    }

    public void BtnExitShowPopup()
    {
        if (isShowing)
            return;

        Managers.UI.ClosePopupUI();
        Debug.Log("Exit");
    }
}
