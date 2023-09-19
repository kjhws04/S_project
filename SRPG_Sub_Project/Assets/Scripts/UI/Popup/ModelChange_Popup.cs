using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModelChange_Popup : UI_Popup
{
    public GameObject[] slot;

    UserData _userData;
    MainScene _main;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        _main = FindObjectOfType<MainScene>();
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();

        CharacterShow();
    }

    private void CharacterShow()
    {
        List<Stat> _statList = new List<Stat>(_userData._userCharData.Values).OrderByDescending(character => character.Rank).ToList();

        for (int i = 0; i < _statList.Count; i++)
        {
            string key_WeaponName = _userData._userCharData.Keys.ElementAt(i);
            slot[i].SetActive(true);
            slot[i].GetComponent<Image>().sprite = _statList[i].proflieImg;
        }
    }

    public void BtnExit()
    {
        Managers.UI.ClosePopupUI();
    }
}
