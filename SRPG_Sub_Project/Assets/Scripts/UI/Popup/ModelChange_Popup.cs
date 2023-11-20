using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// <summary>
// 메인 화면에서 모델 전환 버튼을 눌렸을 때 popup
// </summary>
public class ModelChange_Popup : UI_Popup
{
    public GameObject[] slot;

    UserData _userData;

    private void Start()
    {
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();

        CharacterShow();
    }

    // <summary>
    // 딕셔너리에서 character의 정보를 가져오는 함수
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
    // 모델로 설정할 캐릭터의 정보를 카피
    // </summary>
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
