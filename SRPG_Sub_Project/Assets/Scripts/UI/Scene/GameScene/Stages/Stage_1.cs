using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stage_1 : BaseScene
{
    UserData _userData;

    public List<Transform> _unit = new List<Transform>();
    public List<Stat> _p1UnitList = new List<Stat>();
    public List<Stat> _p2UnitList = new List<Stat>();
    public List<Stat> _p3UnitList = new List<Stat>();

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.BattleField;

        _userData = Managers.Game.GetUserData().GetComponent<UserData>();

        Managers.Battle.UnitSetting(_unit, _p1UnitList, _p2UnitList, _p3UnitList);
        //Managers.Stage.LoadMap(1);
    }

    public override void Clear()
    {

    }

    public void BtnMain()
    {
        Managers.Scene.LoadScene(Define.Scene.Main);
    }
}
