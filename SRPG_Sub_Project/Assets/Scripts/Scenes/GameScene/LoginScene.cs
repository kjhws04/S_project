using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : BaseScene
{
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;
    }

    public override void Clear()
    {

    }
}
