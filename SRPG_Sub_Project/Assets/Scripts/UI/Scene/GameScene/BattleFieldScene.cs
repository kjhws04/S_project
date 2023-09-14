using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldScene : BaseScene
{
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        SceneType = Define.Scene.BattleField;
    }

    public override void Clear()
    {

    }
}
