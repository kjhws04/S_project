using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        _chooseData = ExtractSwordWeapons(_userData._userWeaponData, _weaponClass);
        ShowWeaponCard(_chooseData);
    }

    // WeaponType이 _weaponClass인 데이터 추출하는 함수
    private Dictionary<string, WeaponStat> ExtractSwordWeapons(Dictionary<string, WeaponStat> dictionary, Class.WeaponClass type)
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

    private void ShowWeaponCard(Dictionary<string, WeaponStat> weaponType)
    {
        List<WeaponStat> sortedWeapons = weaponType.Values.OrderByDescending(stat => stat.rank).ToList();

        for (int i = 0; i < Mathf.Min(Slot.Length, sortedWeapons.Count); i++)
        {
            Slot[i].SetActive(true);
            Slot[i].GetComponent<Image>().sprite = sortedWeapons[i].weaponCardImg;
        }
    }

    public override void Init()
    {
        base.Init();
    }

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
