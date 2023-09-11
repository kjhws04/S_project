using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Recall_Base : UI_Popup
{
    public Define.RecallType _type = Define.RecallType.None;

    enum Buttons
    {
        Recall1_Btn,
        Recall10_Btn
    }

    public override void Init()
    {
        base.Init();

        #region Bind
        Bind<Button>(typeof(Buttons));
        #endregion


        GetButton((int)Buttons.Recall1_Btn).gameObject.AddUIEvent(BtnRecall1Time);
        GetButton((int)Buttons.Recall10_Btn).gameObject.AddUIEvent(BtnRecall10Time);
    }

    public void BtnRecall1Time(PointerEventData data)
    {
        Debug.Log("1회 소환");
        Managers.UI.ShowPopupUI<Recall_Show_Popup>();
    }

    public void BtnRecall10Time(PointerEventData data)
    {
        Debug.Log("10회 소환");
        Recall_Show_Popup popup = Managers.UI.ShowPopupUI<Recall_Show_Popup>();
        popup.recallTime = 10;
    }
}
