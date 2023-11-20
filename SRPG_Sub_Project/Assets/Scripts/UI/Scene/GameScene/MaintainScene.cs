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

        Util.ChangeResolution();

        _userData = Managers.Game.GetUserData().GetComponent<UserData>();

        List<WeaponStat> _statList = new List<WeaponStat>(_userData._userWeaponData.Values);
        _weaponList = _statList;
        Sorting(_weaponList);
        Managers.Sound.Play("BGM_04", Define.Sound.Bgm);
    }

    // <summary>
    // 무기리스트를 정렬하는 버튼 함수 (성급별, 클래스별, 등등 todo)
    // </summary>
    public void BtnSoring()
    {
        Sorting(_weaponList);
    }

    // <summary>
    // 무기리스트를 자동으로 정렬하는 함수
    // </summary>
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
