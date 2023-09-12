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

    GameObject model;

    enum Texts
    {
        Soldier_Name,
    }

    enum Images
    {
        Character_Model,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Main;

        #region Bind
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        #endregion

        UserData _data = Managers.Game.GetUserData().GetComponent<UserData>();

        GetTextMeshProUGUI((int)Texts.Soldier_Name).text = _data.UserName;
        model = GetImage((int)Images.Character_Model).gameObject;
    }

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

    //Clear 부분
    public override void Clear()
    {
    }

    //버튼
    #region Buttons
    public void BtnBattleField()
    {
        Managers.Scene.LoadScene(Define.Scene.BattleField);
    }

    public void BtnRecallField()
    {
        Managers.Scene.LoadScene(Define.Scene.Recall);
    }

    public void BtnEventField()
    {
        //TODO
    }
    #endregion
}
