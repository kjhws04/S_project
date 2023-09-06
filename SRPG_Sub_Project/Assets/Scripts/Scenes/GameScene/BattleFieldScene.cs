using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldScene : BaseScene
{
    private void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.BattleField;
    }

    public override void Clear()
    {

    }
}
