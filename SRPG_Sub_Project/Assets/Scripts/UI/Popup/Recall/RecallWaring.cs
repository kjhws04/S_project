using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// Ƽ�� ������ ������ ��� popup�Ǵ� â
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

