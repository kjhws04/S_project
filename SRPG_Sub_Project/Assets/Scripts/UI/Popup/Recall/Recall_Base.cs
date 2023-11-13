using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Threading.Tasks;

public class Recall_Base : UI_Popup
{
    public Define.RecallType _type;
    bool isHave = false;
    UserData _data;
    RecallScene _recall;
    int ticketCount;

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
    // 가챠 1회분, 가챠 10회분
    // </summary>
    #region RecallTime
    public void BtnRecall1Time(PointerEventData data)
    {
        int recallTime = 1;
        Btn(recallTime);
    }

    public void BtnRecall10Time(PointerEventData data)
    {
        int recallTime = 10;
        Btn(recallTime);
    }

    public async void Btn(int recallTime)
    {
        await CheckTicketCount();
        CheckRecallTicketType(recallTime);
        if (!isHave)
            return;

        Recall_Show_Popup popup = Managers.UI.ShowPopupUI<Recall_Show_Popup>();
        NowRecall(popup, recallTime);
        isHave = false;

        _data.Gacha += recallTime;
        Managers.Mission.ConditionComparison(Define.MissionType.GachaCount, _data.Gacha);
        _recall.ResetTicket();
    }

    //티켓 개수 확인
    async Task CheckTicketCount()
    {
        try
        {
            ticketCount = 0;
            switch (_type)
            {
                case Define.RecallType.Ticket1:
                    ticketCount = await Managers.Fire.GetItemCountAsync("_ticket1");
                    break;
                case Define.RecallType.Ticket2:
                    ticketCount = await Managers.Fire.GetItemCountAsync("_ticket2");
                    break;
                case Define.RecallType.FriendTicket:
                    ticketCount = await Managers.Fire.GetItemCountAsync("_ticketFriend");
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
        }
    }

    //db의 티켓 갯수를 확인하고 실제 차감
    private void CheckRecallTicketType(int recallTime)
    {
        try
        {
            switch (_type)
            {
                case Define.RecallType.Ticket1:
                    if (ticketCount >= recallTime)
                    {
                        Managers.Fire.SaveItems("_ticket1", ticketCount - recallTime);
                        isHave = true;
                    }
                    else
                        Managers.UI.ShowPopupUI<RecallWaring>();
                    break;
                case Define.RecallType.Ticket2:
                    if (ticketCount >= recallTime)
                    {
                        Managers.Fire.SaveItems("_ticket2", ticketCount - recallTime);
                        isHave = true;
                    }
                    else
                        Managers.UI.ShowPopupUI<RecallWaring>();
                    break;
                case Define.RecallType.FriendTicket:
                    if (ticketCount >= recallTime)
                    {
                        Managers.Fire.SaveItems("_ticketFriend", ticketCount - recallTime);
                        isHave = true;
                    }
                    else
                        Managers.UI.ShowPopupUI<RecallWaring>();
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("An error occurred: " + e.Message);
        }
    }

    //void CheckRecallTicketType(int recallTime)
    //{
    //    switch (_type)
    //    {
    //        case Define.RecallType.Ticket1:
    //            if (_data.Ticket1 >= recallTime)
    //                _data.Ticket1 -= recallTime;
    //            else
    //            {
    //                Managers.UI.ShowPopupUI<RecallWaring>();
    //                isHave = false;
    //            }
    //            break;
    //        case Define.RecallType.Ticket2:
    //            if (_data.Ticket2 >= recallTime)
    //                _data.Ticket2 -= recallTime;
    //            else
    //            {
    //                Managers.UI.ShowPopupUI<RecallWaring>();
    //                isHave = false;
    //            }
    //            break;
    //        case Define.RecallType.FriendTicket:
    //            if (_data.TicketFriend >= recallTime)
    //                _data.TicketFriend -= recallTime;
    //            else
    //            {
    //                Managers.UI.ShowPopupUI<RecallWaring>();
    //                isHave = false;
    //            }
    //            break;
    //    }
    //}
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
