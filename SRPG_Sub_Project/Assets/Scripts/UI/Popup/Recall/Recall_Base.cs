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
    // ��í 1ȸ��, ��í 10ȸ�� ��ư�� ���� �Լ�
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

    // <summary>
    // ��ư���� �⺻ ���� �Լ�
    // </summary>
    public async void Btn(int recallTime)
    {
        await CheckTicketCount(); //Ƽ�� ���� Ȯ��

        CheckRecallTicketType(recallTime); // db���� reacllTime��ŭ ���� (isHave ���� ��ȭ)
         
        if (!isHave) //Ƽ�� ����
            return;

        Recall_Show_Popup popup = Managers.UI.ShowPopupUI<Recall_Show_Popup>(); // ��í ȭ�� ���
        NowRecall(popup, recallTime); // ��í ȭ�� ����
        isHave = false;

        _data.Gacha += recallTime; // �̼ǿ�
        Managers.Mission.ConditionComparison(Define.MissionType.GachaCount, _data.Gacha);
        _recall.ResetTicket(); // Ƽ�� ���� ȭ�鿡 ����
    }

    // <summary>
    // firebase�� Ƽ�� ���� Ȯ��
    // </summary>
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

    // <summary>
    // firebase�� Ƽ�� ������ Ȯ���ϰ� ���� ����
    // </summary>
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

    // <summary>
    // ��í �׸��� ���� ���� ��ȯ
    // </summary>
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
                _popup.FriendRecall(_recallTime);
                break;
        }
    }
    #endregion
}
