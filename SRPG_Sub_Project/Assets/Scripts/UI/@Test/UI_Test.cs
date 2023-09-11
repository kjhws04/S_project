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
        base.Init(); //UI_popup(UI_scene)�� Init(sort�� �����ϴ� �Լ�)�� ���� ���
        #region Bind
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
        #endregion

        // <surmmary>
        // �̺�Ʈ�� �߰��ϴ� Code
        // �̹���(TestImage)�� �巡�� ���� ��, �̹����� position��, data(���콺)�� position���� �̵� 
        // <surmmary>
        GameObject go = GetImage((int)Images.TestImage).gameObject;
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

        // <surmmary>
        // �̺�Ʈ�� �߰��ϴ� Code
        // ��ư(TestButton)�� Ŭ�� ���� ��, ��ư�� �Լ� (OnButtonClicked)�� ����
        // <surmmary>
        GetButton((int)Buttons.TestButton).gameObject.AddUIEvent(OnButtonClicked);
    }

    #region MoveToOrtherScene
    // <surmmary>
    // �� �̵��� �ϴ� �Լ�
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

    //Memo : �Լ��� ���� ���ҽ� ó���� �ʿ��� (loading)�� �̵��� �񵿱� ������� background�� �ε�
    //SceneManager.LoadSceneAsync() 
    #endregion
}
