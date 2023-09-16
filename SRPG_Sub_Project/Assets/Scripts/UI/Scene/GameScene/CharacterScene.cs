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
    List<Stat> _charStat;

    enum Images
    {
        Char_Select_Model, 
        Weapon_Img
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
        _charStat = _statList;

        Sorting(_statList);
        //ModelInfoChange(_stat);
    }

    //정렬
    private void Sorting(List<Stat> _list)
    {
        List<Stat> list = _list.OrderByDescending(character => character.Rank).ToList();

        for (int i = 0; i < list.Count; i++)
        {
            string key_WeaponName = _userData._userCharData.Keys.ElementAt(i);
            _stat = list[i];
            SaveCharInfo(_stat, i);
            _charSlot[i].gameObject.GetComponent<Image>().sprite = _stat.proflieImg;
        }
        ModelInfoChange(list[0]);
    }

    public override void Clear()
    {
        //TODO 정보 초기화
    }

    //값이 바뀌었을 때, userDic에 저장
    public void ModifyStat(string _charName, Stat _stat)
    {
        //userCharData에 있는 Stat 값을 가져와서
        //값을 변경
        if(_userData._userCharData.ContainsKey(_charName))
        {
            //TODO
        }
    }

    public void SaveCharInfo(Stat _stat, int i)
    {
        Stat _copy = _charSlot[i].gameObject.AddComponent<Stat>();
        ChangeStat(_copy, _stat);
    }

    // _orgStat = UserCharDic에 있는 스텟, _changeStat 값을 바꿀 스텟
    public void ChangeStat(Stat _orgStat, Stat _changeStat)
    {
        _orgStat.Name = _changeStat.Name;
        _orgStat.CurClass = _changeStat.CurClass;
        _orgStat.WeaponClass = _changeStat.WeaponClass;
        _orgStat.Hp = _changeStat.Hp;
        _orgStat.Str = _changeStat.Str;
        _orgStat.Int = _changeStat.Int;
        _orgStat.Tec = _changeStat.Tec;
        _orgStat.Spd = _changeStat.Spd;
        _orgStat.Def = _changeStat.Def;
        _orgStat.MDef = _changeStat.MDef;
        _orgStat.Luk = _changeStat.Luk;
        _orgStat.Wei = _changeStat.Wei;
        _orgStat.Level = _changeStat.Level;
        _orgStat.modelImg = _changeStat.modelImg;
    }

    public void ModelInfoChange(Stat _changeStat)
    {
        _stat = _changeStat;
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
    public void BtnChangeWeapon()
    {
        Managers.UI.ShowPopupUI<WeaponSlot_Popup>().SaveCharType(_stat);
    }
}
