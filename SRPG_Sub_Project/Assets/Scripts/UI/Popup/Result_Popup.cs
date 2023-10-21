using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result_Popup : UI_Popup
{
    public GameObject winBase;
    public GameObject loseBase;
    public Sprite expItem; //아이템 추가시, 배열로 변경
    public GameObject[] Slot;

    UserData _data;

    public override void Init()
    {
        base.Init();

        winBase = Util.FindChild(gameObject, "Win_Base");
        loseBase = Util.FindChild(gameObject, "Lose_Base");
        #region Mapping
        #endregion
    }

    // <summary>
    // 외부(Stat) 호출, 승리했을 때 함수
    // </summary>
    public void Win(int count)
    {
        _data = Managers.Game.GetUserData().GetComponent<UserData>();
        winBase.SetActive(true);
        Reword(count);
    }

    // <summary>
    // 승리 시, 보상을 정리하는 함수 (ExpBook)
    // </summary>
    public void Reword(int count)
    {
        _data.ExpItem += count; //아이템 책 data에 추가
        expItem = Resources.Load<Sprite>("Item/ExpBook"); //아이템 책 이미지
        for (int i = 0; i < 1; i++)
        {
            expItem = Slot[i].transform.GetChild(0).GetComponent<Image>().sprite;
            Debug.Log("Tes");
        }

        Debug.Log("Test");
    }

    // <summary>
    // 외부(Stat) 호출, 패배했을 때 함수
    // </summary>
    public void Lose()
    {
        loseBase.SetActive(true);
    }

    public void ExitBtn()
    {
        Managers.UI.ClosePopupUI();
        Managers.Scene.LoadScene(Define.Scene.BattleField);
    }
}
