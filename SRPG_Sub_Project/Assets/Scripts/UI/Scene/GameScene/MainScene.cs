using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        #region Bind
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        #endregion

        GetTextMeshProUGUI((int)Texts.Soldier_Name).text = _data.UserName;
        model = GetImage((int)Images.Character_Model).gameObject;

        if (_data._modelImg != null)
            GetImage((int)Images.Character_Model).sprite = _data._modelImg;
        if (_data._backGroundImg != null)
            GetImage((int)Images.BackGround).sprite = _data._backGroundImg;
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

    #region Buttons
    //모델 바꾸기
    public void BtnChangeModel()
    {
        _data.ChangeModel();
    }
    //전장
    public void BtnBattle()
    {
        Managers.Scene.LoadScene(Define.Scene.BattleField);
    }
    //소환
    public void BtnRecall()
    {
        Managers.Scene.LoadScene(Define.Scene.Recall);
    }
    //이벤트
    public void BtnEventField()
    {
        _userData.Stage = 999;
        Managers.Scene.LoadScene(Define.Scene.Stage_1);
    }
    //장비
    public void BtnMaintain()
    {
        Managers.Scene.LoadScene(Define.Scene.Maintain);
    }
    //미션
    public void BtnMission()
    {
        Managers.UI.ShowPopupUI<Mission_Popup>();
    }
    //정비
    public void BtnCharacter()
    {
        Managers.Scene.LoadScene(Define.Scene.Character);
    }
    #endregion

    //Clear 부분
    public override void Clear()
    {

    }
}
