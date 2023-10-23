using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp_Popup : UI_Popup
{
    //uesd Component
    UserData _userData;
    Stat _stat;

    #region Mapping Things

    enum Texts
    {
        Level_Txt,
        Hp,
        Str,
        Int,
        Tec,
        Spd,
        Def,
        Mdf,
        Luk,
        Wei
    }

    enum GameObjects
    {
        LevelUpTitleText
    }
    #endregion

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        #region Bind
        Bind<GameObject>(typeof(GameObjects));
        #endregion

        GetObject((int)GameObjects.LevelUpTitleText).GetComponent<Animator>().Play("LevelUpTextAnim");
    }

    public void StatSetting(Stat _charStst)
    {
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        Bind<TextMeshProUGUI>(typeof(Texts));

        _stat = _charStst;
        GetTextMeshProUGUI((int)Texts.Level_Txt).text = $"{_stat.Level}";
        GetTextMeshProUGUI((int)Texts.Hp).text = $"{_stat.Hp}";
        GetTextMeshProUGUI((int)Texts.Str).text = $"{_stat.Str}";
        GetTextMeshProUGUI((int)Texts.Int).text = $"{_stat.Int}";
        GetTextMeshProUGUI((int)Texts.Tec).text = $"{_stat.Tec}";
        GetTextMeshProUGUI((int)Texts.Spd).text = $"{_stat.Spd}";
        GetTextMeshProUGUI((int)Texts.Def).text = $"{_stat.Def}";
        GetTextMeshProUGUI((int)Texts.Mdf).text = $"{_stat.MDef}";
        GetTextMeshProUGUI((int)Texts.Luk).text = $"{_stat.Luk}";
        GetTextMeshProUGUI((int)Texts.Wei).text = $"{_stat.Wei}";

        LevelUpAfterStat();
    }

    public void LevelUpAfterStat()
    {
        _userData._userCharData[_stat.Name].Hp += 1;
        Debug.Log("test");
    }

    public void ClosePopupBtn()
    {
        Managers.UI.ClosePopupUI();
    }
}