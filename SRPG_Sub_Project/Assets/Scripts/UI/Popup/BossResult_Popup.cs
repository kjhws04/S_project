using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// <summary>
// 보스전 결과창
// </summary>
public class BossResult_Popup : UI_Popup
{
    enum Texts
    {
        Score,
        High
    }

    public async override void Init()
    {
        base.Init();

        #region Mapping
        Bind<TextMeshProUGUI>(typeof(Texts));
        await Managers.Fire.SaveHighScore("Boss1", Managers.Stage.highDamage);
        int highScore = await Managers.Fire.GetHighScore("Boss1");

        // db의 최고 기록을 기록
        GetTextMeshProUGUI((int)Texts.Score).text = $"Score : {Managers.Stage.totalDamage}";
        GetTextMeshProUGUI((int)Texts.High).text = $"High : {highScore}";
        #endregion
        Managers.Stage.totalDamage = 0;
    }

    public void ExitBtn()
    {
        Managers.UI.ClosePopupUI();
        Managers.Scene.LoadScene(Define.Scene.Main);
    }
}