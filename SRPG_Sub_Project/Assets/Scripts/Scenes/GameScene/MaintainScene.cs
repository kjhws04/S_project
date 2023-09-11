using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainScene : BaseScene
{
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Maintain;
    }

    public override void Clear()
    {

    }
}
