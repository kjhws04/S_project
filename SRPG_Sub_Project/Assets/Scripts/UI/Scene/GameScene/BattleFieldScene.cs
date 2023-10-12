using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleFieldScene : BaseScene
{
    enum Buttons
    {
        Stage_1,
        Stage_2,
        Stage_3,
        Stage_4,
        Stage_5
    } //Buttons Bind List�� ��� �߰�

    public Image[] _stageImage;

    UserData _userData;

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.BattleField;
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        StageImageSetting();

        #region Bind
        Bind<Button>(typeof(Buttons));
        #endregion

        #region Buttons Bind List
        //< surmmary >
        // ��ư ���ε�, ��ư�� �Լ�() ����
        // <surmmary>
        GetButton((int)Buttons.Stage_1).gameObject.AddUIEvent(BtnStage1);
        GetButton((int)Buttons.Stage_2).gameObject.AddUIEvent(BtnStage2);
        GetButton((int)Buttons.Stage_3).gameObject.AddUIEvent(BtnStage3);
        GetButton((int)Buttons.Stage_4).gameObject.AddUIEvent(BtnStage4);
        GetButton((int)Buttons.Stage_5).gameObject.AddUIEvent(BtnStage5);
        #endregion
    }

    //���� �������� _stageCount�� ��ġ�� ���� �̹��� ���� ��ȭ
    private void StageImageSetting()
    {
        for (int i = 0; i < _stageImage.Length; i++)
        {
            _stageImage[i].color = (_userData._stageCount <= i) ? Color.gray : Color.white;
        }
    }

    public override void Clear()
    {

    }

    #region Buttons List
    public void BtnStage1(PointerEventData data)
    {
        BtnStage(1, Define.Scene.Stage_1);
    }
    public void BtnStage2(PointerEventData data)
    {
        BtnStage(2, Define.Scene.Stage_2);
    }
    public void BtnStage3(PointerEventData data)
    {
        BtnStage(3, Define.Scene.Stage_3);
    }
    public void BtnStage4(PointerEventData data)
    {
        BtnStage(4, Define.Scene.Stage_4);
    }
    public void BtnStage5(PointerEventData data)
    {
        BtnStage(5, Define.Scene.Stage_5);
    }

    public void BtnStage(int stageNumber, Define.Scene sceneName)
    {
        if (_userData._stageCount >= stageNumber)
        {
            Managers.Scene.LoadScene(sceneName);
        }
    }
    public void BtnMain() { Managers.Scene.LoadScene(Define.Scene.Main); }
    #endregion
}