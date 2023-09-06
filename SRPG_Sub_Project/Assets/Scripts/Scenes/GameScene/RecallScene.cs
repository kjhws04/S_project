using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallScene : BaseScene
{
    private void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Recall;
    }

    public override void Clear()
    {

    }
}
