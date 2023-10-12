using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MaintainScene : BaseScene
{
    [SerializeField] private GameObject[] maintainSlot;
    UserData _userData;
    WeaponStat _stat;
    List<WeaponStat> _weaponList;

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Maintain;

        _userData = Managers.Game.GetUserData().GetComponent<UserData>();

        List<WeaponStat> _statList = new List<WeaponStat>(_userData._userWeaponData.Values);
        _weaponList = _statList;
        Sorting(_weaponList);
    }

    public void BtnSoring()
    {
        Sorting(_weaponList);
    }

    private void Sorting(List<WeaponStat> _list)
    {
        List<WeaponStat> list = _list.OrderByDescending(weapon => weapon.rank).ToList();

        for (int i = 0; i < list.Count; i++)
        {
            string key_WeaponName = _userData._userWeaponData.Keys.ElementAt(i);
            _stat = list[i];
            maintainSlot[i].SetActive(true);
            maintainSlot[i].gameObject.GetComponent<Image>().sprite = _stat.weaponCardImg;
        }
    }

    public override void Clear()
    {

    }

    public void BtnMain()
    {
        Managers.Scene.LoadScene(Define.Scene.Main);
    }

}
