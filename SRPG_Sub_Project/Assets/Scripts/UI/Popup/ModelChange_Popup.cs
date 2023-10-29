using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ModelChange_Popup : UI_Popup
{
    public GameObject[] slot;

    UserData _userData;

    private void Start()
    {
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

            #region Copy Component
            CopyFrom(_statList[i], i);
            #endregion
        }
    }

    public void CopyFrom(Stat _stat, int i)
    {
        Stat copyStat = slot[i].AddComponent<Stat>();
        copyStat.proflieImg = _stat.proflieImg;
        copyStat.modelImg = _stat.modelImg;
        copyStat.backGroundImg = _stat.backGroundImg;

        slot[i].GetComponent<Image>().sprite = copyStat.proflieImg;
    }

    public void BtnExit()
    {
        Managers.UI.ClosePopupUI();
    }
}
