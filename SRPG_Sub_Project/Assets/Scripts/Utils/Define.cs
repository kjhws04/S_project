using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    // <surmmary>
    // 게임을 구성하는 Scene창
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
    // UIEvent에 적용될 enum 값
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
