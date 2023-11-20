using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// <summary>
// 캐릭터 화면에서 무기 버튼을 눌렸을 때 popup
// </summary>
public class WeaponSlot_Popup : UI_Popup
{
    public UserData _userData;
    public GameObject[] Slot;
    Stat stat;

    Class.WeaponClass _weaponClass;
    Dictionary<string, WeaponStat> _chooseData;
    List<WeaponStat> _chooseList;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        _chooseData = ExtractWeapons(_userData._userWeaponData, _weaponClass);
        ShowWeaponCard(_chooseData);
    }

    // <summary>
    // WeaponType이 _weaponClass인 데이터 추출하는 함수
    // </summary>
    private Dictionary<string, WeaponStat> ExtractWeapons(Dictionary<string, WeaponStat> dictionary, Class.WeaponClass type)
    {
        Dictionary<string, WeaponStat> weaponType = new Dictionary<string, WeaponStat>();

        foreach (var kvp in dictionary)
        {
            if (kvp.Value.weaponType == type)
            {
                weaponType.Add(kvp.Key, kvp.Value);
            }
        }

        return weaponType;
    }

    // <summary>
    // 리스트를 돌아 rank 순으로 정렬하고, 무기 카드를 보여주는 함수
    // </summary>
    private void ShowWeaponCard(Dictionary<string, WeaponStat> weaponType)
    {
        List<WeaponStat> sortedWeapons = weaponType.Values.OrderByDescending(stat => stat.rank).ToList();

        for (int i = 0; i < Mathf.Min(Slot.Length, sortedWeapons.Count); i++)
        {
            Slot[i].SetActive(true);
            Slot[i].GetComponent<Image>().sprite = sortedWeapons[i].weaponCardImg;
        }
        _chooseList = sortedWeapons;
        ResetSlotColor();
    }

    // <summary>
    // 무기가 사용중이면 회색으로, 비 사용중이면 원래대로 돌리는 함수
    // </summary>
    public void ResetSlotColor()
    {
        for (int i = 0; i < Mathf.Min(Slot.Length, _chooseList.Count); i++)
        {
            if (_chooseList[i].isUsed)
            {
                Slot[i].GetComponent<Image>().color = Color.gray;
            }
            else
            {
                Slot[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    // <summary>
    // 미리 stat을 받아 저장시키는 함수
    // </summary>
    public void SaveCharType(Stat _stat)
    {
        stat = _stat;
        _weaponClass = _stat.WeaponClass;
    }

    public void BtnExit()
    {
        for (int i = 0; i < Slot.Length; i++)
        {
            Slot[i].SetActive(false);
        }
        Managers.UI.ClosePopupUI();
    }
}
