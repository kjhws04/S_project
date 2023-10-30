using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    // <surmmary>
    // 게임을 순서를 결정
    // </surmmary>
    public enum GameStep
    {
        Unknown,
        Setting,
        Battle,
        Result
    }

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
        Character,

        Stage_1,
        Stage_2,
        Stage_3,
        Stage_4,
        Stage_5
    }

    // <surmmary>
    // 미션 성공에 따른 타입
    // </surmmary>
    public enum MissionType
    {
        Unknown,
        StageClear,
        GachaCount,
        LevelUp
    }

    // <surmmary>
    // 게임 타입
    // </surmmary>
    public enum GameType
    {
        Unknown,
        NomalStage,
        BossStage,
    }

    // <surmmary>
    // 소환 재화에 따른 타입
    // </surmmary>
    public enum RecallType
    {
        None,
        Ticket1,
        Ticket2,
        FriendTicket,
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
