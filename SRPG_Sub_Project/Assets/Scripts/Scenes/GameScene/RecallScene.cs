using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallScene : BaseScene
{
    private void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Recall;
        Managers.UI.ShowPopupUI<Recall0_Info_Popup>();
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

    public void BtnExit()
    {
        Managers.Scene.LoadScene(Define.Scene.Main);
    }
}
