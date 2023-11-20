using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharSelSlotBase : MonoBehaviour
{
    public GameObject[] slot;

    UserData _userData;

    private void Start()
    {
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();

        CharacterShow();
    }

    // <summary>
    // 유저데이터로부터 캐릭터 데이터를 가져와 정렬후, 정보를 화면에 표시
    // </summary>
    private void CharacterShow()
    {
        List<Stat> _statList = new List<Stat>(_userData._userCharData.Values).OrderByDescending(character => character.Rank).ToList();

        for (int i = 0; i < _statList.Count; i++)
        {
            string key_WeaponName = _userData._userCharData.Keys.ElementAt(i);
            slot[i].SetActive(true);

            #region Copy Component
            CopyFrom(_statList[i], i);
            #endregion
        }
    }

    // <summary>
    // 유저데이터로부터 캐릭터 데이터의 스텟을 copy하는 함수
    // </summary>
    public void CopyFrom(Stat _stat, int i)
    {
        Stat copyStat = slot[i].AddComponent<Stat>();

        copyStat.modelImg = _stat.modelImg;
        copyStat.cardImage = _stat.cardImage;
        copyStat.proflieImg = _stat.proflieImg;

        copyStat.attackType = _stat.attackType;
        copyStat.Level = _stat.Level;
        copyStat.Name = _stat.Name;

        copyStat.CurClass = _stat.CurClass;
        copyStat.WeaponClass = _stat.WeaponClass;
        copyStat.MainWeapon = _stat.MainWeapon;

        copyStat.Hp = _stat.Hp;
        copyStat.Str = _stat.Str;
        copyStat.Int = _stat.Int;
        copyStat.Tec = _stat.Tec;
        copyStat.Spd = _stat.Spd;
        copyStat.Def = _stat.Def;
        copyStat.MDef = _stat.MDef;
        copyStat.Luk = _stat.Luk;
        copyStat.Wei = _stat.Wei;

        copyStat._unitFR = _stat._unitFR;
        copyStat._unitAR = _stat._unitAR;
        copyStat._unitAS = _stat._unitAS;

        slot[i].GetComponent<Image>().sprite = copyStat.proflieImg;
    }

    public void BtnExit()
    {
        Managers.UI.ClosePopupUI();
    }
}
