using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Database;
using Firebase.Extensions;

public class MainScene : BaseScene
{
    private float speed = 1f;
    private float scaleSpeed = 0.00001f;
    private float length = 0.3f;

    private float runningTime = 0f;
    private float yPos = 0f;

    public GameObject model;
    UserData _userData;

    enum Texts { Soldier_Name }
    enum Images { Character_Model, BackGround }

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Main;
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();

        Util.ChangeResolution();

        #region Bind
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        #endregion

        GetTextMeshProUGUI((int)Texts.Soldier_Name).text = GetUsernameFromEmail(Managers.Fire.user.Email);//_data.UserName;
        model = GetImage((int)Images.Character_Model).gameObject;

        if (_data._modelImg != null)
            GetImage((int)Images.Character_Model).sprite = _data._modelImg;
        if (_data._backGroundImg != null)
            GetImage((int)Images.BackGround).sprite = _data._backGroundImg;

        Managers.Sound.Play("BGM_03", Define.Sound.Bgm);
    }

    public void ChangeModel(Stat stat)
    {
        GetImage((int)Images.Character_Model).sprite = stat.modelImg;
        GetImage((int)Images.BackGround).sprite = stat.backGroundImg;
    }

    //model up down
    private void FixedUpdate()
    {
        runningTime += Time.fixedDeltaTime * speed;
        yPos = Mathf.Sin(runningTime) * length;
        model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y + yPos, model.transform.position.z);
        if (yPos >= 0)
            model.transform.localScale = new Vector2(model.transform.localScale.x + scaleSpeed, model.transform.localScale.y + scaleSpeed);
        else
            model.transform.localScale = new Vector2(model.transform.localScale.x - scaleSpeed, model.transform.localScale.y - scaleSpeed);
    }

    // <summary>
    // 이메일 형식의 string을 받아 아이디 부분만 추출하는 함수
    // </summary>
    private string GetUsernameFromEmail(string email)
    {
        int atIndex = email.IndexOf('@');
        if (atIndex != -1)
        {
            return email.Substring(0, atIndex);
        }
        // 이메일 형식이 아닐시, 그냥 반환
        return email;
    }

    #region Buttons
    //1. 메인 버튼
    //모델 바꾸기
    public void BtnChangeModel()
    {
        _data.ChangeModel();
    }
    //전장 이동
    public void BtnBattle()
    {
        Managers.Scene.LoadScene(Define.Scene.BattleField);
    }
    //소환창 이동
    public void BtnRecall()
    {
        Managers.Scene.LoadScene(Define.Scene.Recall);
    }
    //이벤트창 이동
    public void BtnEventField()
    {
        _userData.Stage = 999;
        Managers.Scene.LoadScene(Define.Scene.Stage_1);
    }
    //장비창 이동
    public void BtnMaintain()
    {
        Managers.Scene.LoadScene(Define.Scene.Maintain);
    }
    //미션 팝업 생성
    public void BtnMission()
    {
        Managers.UI.ShowPopupUI<Mission_Popup>();
    }
    //정비창 이동
    public void BtnCharacter()
    {
        Managers.Scene.LoadScene(Define.Scene.Character);
    }

    //2. 설정 버튼
    //채팅창 열기
    public void Btn_Chat()
    {
        Managers.UI.ShowPopupUI<Chat_Popup>();
    }
    //오디오 조절
    public void Btn_Audio()
    {
        Managers.UI.ShowPopupUI<SoundSetting_Popup>();
    }
    //게임 종료
    public void Btn_Exit()
    {
        Application.Quit();
    }
    #endregion

    //Clear 부분
    public override void Clear()
    {

    }
}
