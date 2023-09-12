using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallWaring : UI_Popup
{
    public override void Init()
    {
        base.Init();
    }

    public void BtnExit()
    {
        Managers.UI.ClosePopupUI();
    }
}

