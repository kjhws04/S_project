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

        Util.ChangeResolution();

        SceneType = Define.Scene.Recall;
        Managers.UI.ShowPopupUI<Recall0_Info_Popup>();
        #region Bind
        Bind<TextMeshProUGUI>(typeof(Texts));
        #endregion
        ResetTicket();
        Managers.Sound.Play("BGM_04", Define.Sound.Bgm);
    }

    // <summary>
    // firebase���� Ƽ���� ������ ������ ������ �ؽ�Ʈ�� ���� ��Ű�� �Լ�
    // </summary>
    public async void ResetTicket()
    {
        #region Mapping
        GetTextMeshProUGUI((int)Texts.Goods1_Txt).text = $"{await Managers.Fire.GetItemCountAsync("_ticket1")}";
        GetTextMeshProUGUI((int)Texts.Goods2_Txt).text = $"{await Managers.Fire.GetItemCountAsync("_ticket2")}";
        GetTextMeshProUGUI((int)Texts.Goods3_Txt).text = $"{await Managers.Fire.GetItemCountAsync("_ticketFriend")}";
        #endregion
    }

    public override void Clear()
    {

    }

    #region Recall List
    //��ȯ �׸��� ��ư
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
    //�Ŀ� ����â�� ���� �κ�
    #region Shop List 
    public async void BtnShop1()
    {
        await Managers.Fire.SaveItemsAsync("_ticket1", 10);
        ResetTicket();
    }
    public async void BtnShop2()
    {
        await Managers.Fire.SaveItemsAsync("_ticket2", 10);
        ResetTicket();
    }
    public async void BtnShop3()
    {
        await Managers.Fire.SaveItemsAsync("_ticketFriend", 10);
        ResetTicket();
    }
    #endregion
    public void BtnExit()
    {
        Managers.Scene.LoadScene(Define.Scene.Main);
        ResetTicket();
    }
}
