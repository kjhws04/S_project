using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// 티켓 개수가 부족한 경우 popup되는 창
// </summary>
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

