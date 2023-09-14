using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : UI_Base
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown; //get은 자유, set은 자식 component만,
    public UserData _data;

    public virtual void Init()
    {
        #region EventSystem Setting
        // <summary>
        // EventSysyem이 없다면, UI의 기능이 실행되지 않음. 새로운 Scene을 Init할 때, Hierarchy에 EventSysytem을 확인
        // </summary>
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        #endregion

        _data = Managers.Game.GetUserData().GetComponent<UserData>();
    }

    public abstract void Clear();
}
