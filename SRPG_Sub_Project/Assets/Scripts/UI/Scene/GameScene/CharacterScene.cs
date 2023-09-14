using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScene : BaseScene
{
    public CharacterSlot[] _charSlot;

    UserData _userData;
    Stat _stat;

    enum Images
    {
        Char_Select_Model
    }

    enum Texts 
    {
        Char_Name,
        Char_ClassnWeapon,
        Hp,
        Str,
        Int,
        Tec,
        Spd,
        Def,
        Mdf,
        Luk,
        Wei,
        Lv
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        #region Bind
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        #endregion

        SceneType = Define.Scene.Character;

        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        List<Stat> _statList = new List<Stat>(_userData._userCharData.Values);
        for (int i = 0; i < _statList.Count; i++)
        {
            string key = _userData._userCharData.Keys.ElementAt(i);
            _stat = _statList[i];
            _charSlot[i].gameObject.GetComponent<Image>().sprite = _stat.proflieImg;
        }

        ModelInfoChange(_stat);
    }

    public override void Clear()
    {
        //TODO 정보 초기화
    }

    public void ModelInfoChange(Stat _changeStat)
    {
        GetTextMeshProUGUI((int)Texts.Char_Name).text = _changeStat.Name;
        GetTextMeshProUGUI((int)Texts.Char_ClassnWeapon).text = $"{_changeStat.CurClass} / {_changeStat.WeaponClass}";
        GetTextMeshProUGUI((int)Texts.Hp).text = $"{_changeStat.Hp}";
        GetTextMeshProUGUI((int)Texts.Str).text = $"{_changeStat.Str}";
        GetTextMeshProUGUI((int)Texts.Int).text = $"{_changeStat.Int}";
        GetTextMeshProUGUI((int)Texts.Tec).text = $"{_changeStat.Tec}";
        GetTextMeshProUGUI((int)Texts.Spd).text = $"{_changeStat.Spd}";
        GetTextMeshProUGUI((int)Texts.Def).text = $"{_changeStat.Def}";
        GetTextMeshProUGUI((int)Texts.Mdf).text = $"{_changeStat.MDef}";
        GetTextMeshProUGUI((int)Texts.Luk).text = $"{_changeStat.Luk}";
        GetTextMeshProUGUI((int)Texts.Wei).text = $"{_changeStat.Wei}";
        GetTextMeshProUGUI((int)Texts.Lv).text = $"Lv. {_changeStat.Level}";
        GetImage((int)Images.Char_Select_Model).sprite = _changeStat.modelImg;
    }

    public void BtnMain()
    {
        Managers.Scene.LoadScene(Define.Scene.Main);
    }
}
