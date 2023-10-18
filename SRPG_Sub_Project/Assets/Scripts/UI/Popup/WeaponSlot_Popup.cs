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
    }

    public override void Init()
    {
        base.Init();
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        _chooseData = ExtractWeapons(_userData._userWeaponData, _weaponClass);
        ShowWeaponCard(_chooseData);
    }

    // WeaponType�� _weaponClass�� ������ �����ϴ� �Լ�
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