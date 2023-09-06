using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainScene : BaseScene
{
    private void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Maintain;
    }

    public override void Clear()
    {

    }
}
