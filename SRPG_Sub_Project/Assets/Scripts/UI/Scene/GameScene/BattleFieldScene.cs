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
    } //Buttons Bind List에 계속 추가

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
        Util.ChangeResolution();

        StageImageSetting();

        #region Bind
        Bind<Button>(typeof(Buttons));
        #endregion

        #region Buttons Bind List
        //< surmmary >
        // 버튼 바인드, 버튼의 함수() 실행
        // <surmmary>
        GetButton((int)Buttons.Stage_1).gameObject.AddUIEvent(BtnStage1);
        GetButton((int)Buttons.Stage_2).gameObject.AddUIEvent(BtnStage2);
        GetButton((int)Buttons.Stage_3).gameObject.AddUIEvent(BtnStage3);
        GetButton((int)Buttons.Stage_4).gameObject.AddUIEvent(BtnStage4);
        GetButton((int)Buttons.Stage_5).gameObject.AddUIEvent(BtnStage5);
        #endregion
    }

    //유저 데이터의 _stageCount의 수치에 따라 이미지 색상 변화
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
        _userData.Stage = 1;
        BtnStage(_userData.Stage, Define.Scene.Stage_1);
    }
    public void BtnStage2(PointerEventData data)
    {
        _userData.Stage = 2;
        BtnStage(_userData.Stage, Define.Scene.Stage_1);
    }
    public void BtnStage3(PointerEventData data)
    {
        _userData.Stage = 3;
        BtnStage(_userData.Stage, Define.Scene.Stage_1);
    }
    public void BtnStage4(PointerEventData data)
    {
        _userData.Stage = 4;
        BtnStage(_userData.Stage, Define.Scene.Stage_1);
    }
    public void BtnStage5(PointerEventData data)
    {
        _userData.Stage = 5;
        BtnStage(_userData.Stage, Define.Scene.Stage_1);
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