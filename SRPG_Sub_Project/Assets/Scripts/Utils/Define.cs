using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    // <surmmary>
    // ������ ������ ����
    // </surmmary>
    public enum GameStep
    {
        Unknown,
        Setting,
        Battle,
        Result
    }

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
        Character,

        Stage_1,
        Stage_2,
        Stage_3,
        Stage_4,
        Stage_5
    }

    // <surmmary>
    // �̼� ������ ���� Ÿ��
    // </surmmary>
    public enum MissionType
    {
        Unknown,
        StageClear,
        GachaCount,
        LevelUp
    }

    // <surmmary>
    // ���� Ÿ��
    // </surmmary>
    public enum GameType
    {
        Unknown,
        NomalStage,
        BossStage,
    }

    // <surmmary>
    // ��ȯ ��ȭ�� ���� Ÿ��
    // </surmmary>
    public enum RecallType
    {
        None,
        Ticket1,
        Ticket2,
        FriendTicket,
    }

    // <surmmary>
    // UIEvent�� ����� enum ��
    // </surmmary>
    public enum UIEvent
    {
        Click,
        Drag,
    }

    // <surmmary>
    // ������ �����ϴ� Sound ����
    // </surmmary>
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }

    // <surmmary>
    // ����, ����
    // </surmmary>
    public enum AttackType
    {
        Physical,
        Magic,
    }
}
