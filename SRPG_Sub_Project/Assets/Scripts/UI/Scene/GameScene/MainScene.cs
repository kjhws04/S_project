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
    // �̸��� ������ string�� �޾� ���̵� �κи� �����ϴ� �Լ�
    // </summary>
    private string GetUsernameFromEmail(string email)
    {
        int atIndex = email.IndexOf('@');
        if (atIndex != -1)
        {
            return email.Substring(0, atIndex);
        }
        // �̸��� ������ �ƴҽ�, �׳� ��ȯ
        return email;
    }

    #region Buttons
    //1. ���� ��ư
    //�� �ٲٱ�
    public void BtnChangeModel()
    {
        _data.ChangeModel();
    }
    //���� �̵�
    public void BtnBattle()
    {
        Managers.Scene.LoadScene(Define.Scene.BattleField);
    }
    //��ȯâ �̵�
    public void BtnRecall()
    {
        Managers.Scene.LoadScene(Define.Scene.Recall);
    }
    //�̺�Ʈâ �̵�
    public void BtnEventField()
    {
        _userData.Stage = 999;
        Managers.Scene.LoadScene(Define.Scene.Stage_1);
    }
    //���â �̵�
    public void BtnMaintain()
    {
        Managers.Scene.LoadScene(Define.Scene.Maintain);
    }
    //�̼� �˾� ����
    public void BtnMission()
    {
        Managers.UI.ShowPopupUI<Mission_Popup>();
    }
    //����â �̵�
    public void BtnCharacter()
    {
        Managers.Scene.LoadScene(Define.Scene.Character);
    }

    //2. ���� ��ư
    //ä��â ����
    public void Btn_Chat()
    {
        Managers.UI.ShowPopupUI<Chat_Popup>();
    }
    //����� ����
    public void Btn_Audio()
    {
        Managers.UI.ShowPopupUI<SoundSetting_Popup>();
    }
    //���� ����
    public void Btn_Exit()
    {
        Application.Quit();
    }
    #endregion

    //Clear �κ�
    public override void Clear()
    {

    }
}
