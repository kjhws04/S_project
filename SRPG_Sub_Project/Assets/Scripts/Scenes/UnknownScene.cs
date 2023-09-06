using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownScene : BaseScene
{
    private void Start()
    {
        Init();
        //Start는 비활성화 되어 있다면, 기능이 실행되지 않기 때문에,
        //Awake를 사용하면 비활성화 되어 있어도, 기능이 실행된다.
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Unknown;

        Managers.UI.ShowPopupUI<UI_Test>();
    }

    public override void Clear()
    {

    }
}
