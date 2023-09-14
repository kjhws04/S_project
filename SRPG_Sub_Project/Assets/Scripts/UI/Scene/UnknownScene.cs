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

        // json파일의 스텟을 dic으로 옮기는 코드
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
