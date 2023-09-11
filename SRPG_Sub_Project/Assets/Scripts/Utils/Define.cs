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
    // ��ȯ ��ȭ�� ���� Ÿ��
    // </surmmary>
    public enum RecallType
    {
        None,
        Ticket,
        Heart,
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
