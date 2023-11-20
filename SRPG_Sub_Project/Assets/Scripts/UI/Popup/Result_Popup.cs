using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// <summary>
// ��Ʋ ȭ�鿡�� �¸� �Ǵ� �й� ���� �� popup
// </summary>
public class Result_Popup : UI_Popup
{
    public GameObject winBase;
    public GameObject loseBase;
    public Sprite expItem; //������ �߰���, �迭�� ����
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
    // �ܺ�(Stat) ȣ��, �¸����� �� �Լ�
    // </summary>
    public void Win(int count)
    {
        _data = Managers.Game.GetUserData().GetComponent<UserData>();
        winBase.SetActive(true);
        Reword(count);
    }

    // <summary>
    // �¸� ��, ������ �����ϴ� �Լ� (ExpBook)
    // </summary>
    public async void Reword(int count)
    {
        await Managers.Fire.SaveItemsAsync("_expItem", count); //������ å db �߰�

        expItem = Resources.Load<Sprite>("Item/ExpBook"); //������ å �̹���
        for (int i = 0; i < 1; i++)
        {
            expItem = Slot[i].transform.GetChild(0).GetComponent<Image>().sprite;
        }
    }

    // <summary>
    // �ܺ�(Stat) ȣ��, �й����� �� �Լ�
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
