using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// 팝업의 base가 되는 함수 popup class작성 시 사용
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
