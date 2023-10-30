using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossResult_Popup : UI_Popup
{
    enum Texts
    {
        Score,
        High
    }

    public override void Init()
    {
        base.Init();

        #region Mapping
        Bind<TextMeshProUGUI>(typeof(Texts));
        GetTextMeshProUGUI((int)Texts.Score).text = $"Score : {Managers.Stage.totalDamage}";
        GetTextMeshProUGUI((int)Texts.High).text = $"High : {Managers.Stage.highDamage}";
        #endregion
        Managers.Stage.totalDamage = 0;
    }

    public void ExitBtn()
    {
        Managers.UI.ClosePopupUI();
        Managers.Scene.LoadScene(Define.Scene.Main);
    }
}