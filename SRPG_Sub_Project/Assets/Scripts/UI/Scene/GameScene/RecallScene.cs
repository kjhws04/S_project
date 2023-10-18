using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecallScene : BaseScene
{
    enum Texts
    {
        Goods1_Txt,
        Goods2_Txt,
        Goods3_Txt
    }

    enum Buttons
    {
        Shop1_Btn,
        Shop2_Btn,
        Shop3_Btn
    }

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Recall;
        Managers.UI.ShowPopupUI<Recall0_Info_Popup>();
        #region Bind
        Bind<TextMeshProUGUI>(typeof(Texts));

        ResetTicket();
        #endregion
    }

    public void ResetTicket()
    {
        #region Mapping
        GetTextMeshProUGUI((int)Texts.Goods1_Txt).text = $"{_data.Ticket1}";
        GetTextMeshProUGUI((int)Texts.Goods2_Txt).text = $"{_data.Ticket2}";
        GetTextMeshProUGUI((int)Texts.Goods3_Txt).text = $"{_data.TicketFriend}";
        #endregion
    }

    public override void Clear()
    {

    }

    #region Recall List
    public void BtnRecall0()
    {
        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<Recall0_Info_Popup>();
    }
    public void BtnRecall1()
    {
        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<Recall1_Info_Popup>();
    }
    public void BtnRecall2()
    {
        Managers.UI.CloseAllPopupUI();
        Managers.UI.ShowPopupUI<Recall2_Info_Popup>();
    }
    public void BtnRecall3()
    {
    }
    public void BtnRecall4()
    {
    }
    #endregion
    #region Shop List
    public void BtnShop1()
    {
        Debug.Log("TODO1");
        ResetTicket();
    }
    public void BtnShop2()
    {
        Debug.Log("TODO2");
        ResetTicket();
    }
    public void BtnShop3()
    {
        Debug.Log("TODO3");
        ResetTicket();
    }
    #endregion
    public void BtnExit()
    {
        Managers.Scene.LoadScene(Define.Scene.Main);
        ResetTicket();
    }
}
