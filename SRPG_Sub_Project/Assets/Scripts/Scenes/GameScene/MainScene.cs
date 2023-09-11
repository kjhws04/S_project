using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : BaseScene
{
    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Main;
    }

    public override void Clear()
    {

    }

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
}
