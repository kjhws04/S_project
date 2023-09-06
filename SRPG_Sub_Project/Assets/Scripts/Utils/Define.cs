using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    // <surmmary>
    // ������ �����ϴ� Sceneâ
    // </surmmary>
    public enum Scene
    {
        Unknown,
        Login,
        Main,
        BattleField,
        Recall,
        Maintain,
    }

    // <surmmary>
    // UIEvent�� ����� enum ��
    // </surmmary>
    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum AttackType
    {
        Physical,
        Magic,
    }
}
