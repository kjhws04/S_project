using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recall1_Info_Popup : Recall_Base
{
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init(); //UI_popup(UI_scene)�� Init(sort�� �����ϴ� �Լ�)�� ���� ���
        _type = Define.RecallType.Ticket2;
        #region Bind
        #endregion
    }
}
