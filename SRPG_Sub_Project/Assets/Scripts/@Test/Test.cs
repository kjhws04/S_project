using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject test;
    void Start()
    {
        // ���� �ý��� �׽�Ʈ
        //StartCoroutine(test());

        // ���ҽ� �Ŵ��� �׽�Ʈ
        //test = Managers.Resource.Instantiate("Test");

        // UI_popup�� show �ϴ� ���
        //Managers.UI.ShowPopupUI<(show �� UI_popup �̸�)>("string ���� �̸�");
        //for (int i = 0; i < 8; i++)
        //    Managers.UI.ShowPopupUI<UI_Test>();
        // UI_popup�� close �ϴ� ���
        //Managers.UI.ClosePopupUI();

        // Sound Test
        //Managers.Sound.Play("(Sound���� ���)/(Sound�̸�)", Define.Sound.BGM); //Bgm
        //Managers.Sound.Play("(Sound���� ���)/(Sound�̸�)"); //Effect
    }

    // ���� �ý��� �׽�Ʈ
    //IEnumerator test()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        Managers.Combat.Init();
    //        new WaitForSeconds(0.1f);
    //    }
    //    yield return null;
    //}

    void Update()
    {
        
    }
}
