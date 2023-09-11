using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    GameObject test;
    void Start()
    {
        // 전투 시스템 테스트
        //StartCoroutine(test());

        // 리소스 매니저 테스트
        //test = Managers.Resource.Instantiate("Test");

        // UI_popup을 show 하는 방법
        //Managers.UI.ShowPopupUI<(show 할 UI_popup 이름)>("string 형식 이름");
        //for (int i = 0; i < 8; i++)
        //    Managers.UI.ShowPopupUI<UI_Test>();
        // UI_popup을 close 하는 방법
        //Managers.UI.ClosePopupUI();

        // Sound Test
        //Managers.Sound.Play("(Sound산하 경로)/(Sound이름)", Define.Sound.BGM); //Bgm
        //Managers.Sound.Play("(Sound산하 경로)/(Sound이름)"); //Effect
    }

    // 전투 시스템 테스트
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
