using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// �˾��� base�� �Ǵ� �Լ� popup class�ۼ� �� ���
// </summary>
public class UI_Popup : UI_Base
{
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
