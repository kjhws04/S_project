using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : UI_Base
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown; //get�� ����, set�� �ڽ� component��,
    public UserData _data;

    public virtual void Init()
    {
        #region EventSystem Setting
        // <summary>
        // EventSysyem�� ���ٸ�, UI�� ����� ������� ����. ���ο� Scene�� Init�� ��, Hierarchy�� EventSysytem�� Ȯ��
        // </summary>
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        #endregion

        _data = Managers.Game.GetUserData().GetComponent<UserData>();
    }

    public abstract void Clear();
}
