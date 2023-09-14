using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownScene : BaseScene
{
    private void Start()
    {
        Init();
        //Start�� ��Ȱ��ȭ �Ǿ� �ִٸ�, ����� ������� �ʱ� ������,
        //Awake�� ����ϸ� ��Ȱ��ȭ �Ǿ� �־, ����� ����ȴ�.

        // json������ ������ dic���� �ű�� �ڵ�
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
    }

    public override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Unknown;

        Debug.Log("Init");
        Managers.UI.ShowPopupUI<UI_Test>();
    }

    public override void Clear()
    {

    }
}
