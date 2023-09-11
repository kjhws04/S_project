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
    // 소환 재화에 따른 타입
    // </surmmary>
    public enum RecallType
    {
        None,
        Ticket,
        Heart,
    }

    // <surmmary>
    // UIEvent에 적용될 enum 값
    // </surmmary>
    public enum UIEvent
    {
        Click,
        Drag,
    }

    // <surmmary>
    // 게임을 구성하는 Sound 종류
    // </surmmary>
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }

    // <surmmary>
    // 물리, 마법
    // </surmmary>
    public enum AttackType
    {
        Physical,
        Magic,
    }
}
