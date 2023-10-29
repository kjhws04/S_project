using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Recall_Base : UI_Popup
{
    public Define.RecallType _type;
    bool isHave = true;
    UserData _data;
    RecallScene _recall;

    enum Buttons
    {
        Recall1_Btn,
        Recall10_Btn
    }

    public override void Init()
    {
        base.Init();

        _recall = FindObjectOfType<RecallScene>();

        #region Bind
        Bind<Button>(typeof(Buttons));
        #endregion

        _data = Managers.Game.GetUserData().GetComponent<UserData>();

        #region Mapping
        GetButton((int)Buttons.Recall1_Btn).gameObject.AddUIEvent(BtnRecall1Time);
        GetButton((int)Buttons.Recall10_Btn).gameObject.AddUIEvent(BtnRecall10Time);
        #endregion
    }

    // <summary>
    // °¡Ã­ 1È¸ºÐ, °¡Ã­ 10È¸ºÐ
    // </summary>
    #region RecallTime
    public void BtnRecall1Time(PointerEventData data)
    {
        int recallTime = 1;
        CheckRecallTicketType(recallTime);
        if (!isHave)
            return;

        Recall_Show_Popup popup = Managers.UI.ShowPopupUI<Recall_Show_Popup>();
        NowRecall(popup, recallTime);
        _recall.ResetTicket();

        _data.Gacha++;
        Managers.Mission.ConditionComparison(Define.MissionType.GachaCount, _data.Gacha);
    }
    public void BtnRecall10Time(PointerEventData data)
    {
        int recallTime = 10;
        CheckRecallTicketType(recallTime);
        if (!isHave)
            return;

        Recall_Show_Popup popup = Managers.UI.ShowPopupUI<Recall_Show_Popup>();
        NowRecall(popup, recallTime);
        _recall.ResetTicket();
        _data.Gacha += 10;
        Managers.Mission.ConditionComparison(Define.MissionType.GachaCount, _data.Gacha);
    }

    void CheckRecallTicketType(int recallTime)
    {
        switch (_type)
        {
            case Define.RecallType.Ticket1:
                if (_data.Ticket1 >= recallTime)
                    _data.Ticket1 -= recallTime;
                else
                {
                    Managers.UI.ShowPopupUI<RecallWaring>();
                    isHave = false;
                }
                break;
            case Define.RecallType.Ticket2:
                if (_data.Ticket2 >= recallTime)
                    _data.Ticket2 -= recallTime;
                else
                {
                    Managers.UI.ShowPopupUI<RecallWaring>();
                    isHave = false;
                }
                break;
            case Define.RecallType.FriendTicket:
                if (_data.TicketFriend >= recallTime)
                    _data.TicketFriend -= recallTime;
                else
                {
                    Managers.UI.ShowPopupUI<RecallWaring>();
                    isHave = false;
                }
                break;
        }
    }
    void NowRecall(Recall_Show_Popup _popup, int _recallTime)
    {
        switch (_type)
        {
            case Define.RecallType.Ticket1:
                _popup.NormalRecall(_recallTime);
                break;
            case Define.RecallType.Ticket2:
                _popup.SpecialRecall(_recallTime);
                break;
            case Define.RecallType.FriendTicket:
                _popup.FrendRecall(_recallTime);
                break;
        }
    }
    #endregion
}
