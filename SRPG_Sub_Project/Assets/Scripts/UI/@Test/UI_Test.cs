using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Test : UI_Popup
{
    enum Buttons
    {
        TestButton,
    }

    enum Texts
    {
        TestText,
    }

    enum Images
    {
        TestImage,
    }

    enum GameObjects
    {
        TestGameObject,
    }

    private void Start()
    {
        Init();

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 5; i++)
            list.Add(Managers.Resource.Instantiate("Test"));

        foreach (GameObject obj in list)
        {
            Managers.Resource.Destroy(obj);
        }
    }

    public override void Init()
    {
        base.Init(); //UI_popup(UI_scene)의 Init(sort를 세팅하는 함수)를 먼저 사용
        #region Bind
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
        #endregion

        // <surmmary>
        // 이벤트를 추가하는 Code
        // 이미지(TestImage)를 드래그 했을 때, 이미지의 position이, data(마우스)의 position으로 이동 
        // <surmmary>
        GameObject go = GetImage((int)Images.TestImage).gameObject;
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

        // <surmmary>
        // 이벤트를 추가하는 Code
        // 버튼(TestButton)을 클릭 했을 때, 버튼의 함수 (OnButtonClicked)가 실행
        // <surmmary>
        GetButton((int)Buttons.TestButton).gameObject.AddUIEvent(OnButtonClicked);
    }

    #region MoveToOrtherScene
    // <surmmary>
    // 씬 이동을 하는 함수
    // <surmmary>
    public void OnButtonClicked(PointerEventData data)
    {
        Debug.Log("Event(Extension) Test!");
        GetTextMeshProUGUI((int)Texts.TestText).text = "Test Complete!";
    }

    public void GoToBattleFieldScene()
    {
        Managers.Scene.LoadScene(Define.Scene.BattleField);
    }
    public void GoToLoginScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Login);
    }
    public void GoToMainScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Main);
    }
    public void GoToMaintainScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Maintain);
    }
    public void GoToRecallScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Recall);
    }

    //Memo : 함수는 많은 리소스 처리가 필요한 (loading)씬 이동을 비동기 방식으로 background로 로딩
    //SceneManager.LoadSceneAsync() 
    #endregion
}
