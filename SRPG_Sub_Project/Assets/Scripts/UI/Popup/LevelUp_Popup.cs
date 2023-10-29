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
        LevelUpTitleText,
        HpPlus,
        StrPlus,
        IntPlus,
        TecPlus,
        SpdPlus,
        DefPlus,
        MdfPlus,
        LukPlus,
        WeiPlus
    }
    #endregion

    // <surmmary>
    // 딕셔너리의 데이터를 받아와 다시 스텟을 setting하는 함수
    // </surmmary>
    public void StatSetting(Stat _charStst)
    {
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();

        #region Bind
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        #endregion

        GetObject((int)GameObjects.LevelUpTitleText).GetComponent<Animator>().Play("LevelUpTextAnim");

        _stat = _charStst;
        StatAnim(_charStst);

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

        _userData.LevelUp++;
        Managers.Mission.ConditionComparison(Define.MissionType.LevelUp, _userData.LevelUp);
    }

    // <surmmary>
    // 성장하는 스텟에 에니메이션 재생
    // </surmmary>
    public void StatAnim(Stat _charStst)
    {
        if (!_charStst.b_Hp) GetObject((int)GameObjects.HpPlus).SetActive(false); else _charStst.b_Hp = false;
        if (!_charStst.b_Str) GetObject((int)GameObjects.StrPlus).SetActive(false); else _charStst.b_Str = false;
        if (!_charStst.b_Int) GetObject((int)GameObjects.IntPlus).SetActive(false); else _charStst.b_Int = false;
        if (!_charStst.b_Tec) GetObject((int)GameObjects.TecPlus).SetActive(false); else _charStst.b_Tec = false;
        if (!_charStst.b_Spd) GetObject((int)GameObjects.SpdPlus).SetActive(false); else _charStst.b_Spd = false;
        if (!_charStst.b_Def) GetObject((int)GameObjects.DefPlus).SetActive(false); else _charStst.b_Def = false;
        if (!_charStst.b_MDef) GetObject((int)GameObjects.MdfPlus).SetActive(false); else _charStst.b_MDef = false;
        if (!_charStst.b_Luk) GetObject((int)GameObjects.LukPlus).SetActive(false); else _charStst.b_Luk = false;

        GetObject((int)GameObjects.WeiPlus).SetActive(false);
    }

    public void ClosePopupBtn()
    {
        Managers.UI.ClosePopupUI();
    }
}