using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// �⺻ �̼� ���� �Լ�
// </summary>
public class Mission
{
    public string missionName; //�̼� �̸�
    public string missionDesc; //�̼� ����
    public int missionCount; //�̼ǿ��� �����ϰ� �ִ� ob�� ī��Ʈ
    public Define.MissionType missionType; //�̼� Ÿ��
    public Define.RecallType itemReward; //���� �̸�
    public int rewardCount; //���� ����

    public bool isMissionComplete; //Ŭ���� ����

    public Mission(string name, string desc, int mCount, Define.MissionType type, Define.RecallType reward, int count)
    {
        missionName = name;
        missionDesc = desc;
        missionCount = mCount;
        missionType = type;
        itemReward = reward;
        rewardCount = count;
        isMissionComplete = false;
    }
}

//BattleManager => Win() : �������� ���� �̼�
//Recall_Base => BtnRecall1(10)Time() : ��í ���� �̼�
//LevelUp_Popup => StatSetting() : ������ ���� �̼�
public class MissionManager
{
    //�̼� ����� ������ �ִ� ����
    public List<Mission> missions = new List<Mission>();
    //������ ����� ���� ����
    private Dictionary<Define.RecallType, Action<int>> rewardActions = new Dictionary<Define.RecallType, Action<int>>();

    UserData _userData;

    public void Init()
    {
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        MissionList();

        //�̸� userData�� ���� ����
        rewardActions.Add(Define.RecallType.Ticket1, (rewardCount) => _userData.Ticket1 += rewardCount);
        rewardActions.Add(Define.RecallType.Ticket2, (rewardCount) => _userData.Ticket2 += rewardCount);
        rewardActions.Add(Define.RecallType.FriendTicket, (rewardCount) => _userData.TicketFriend += rewardCount);
    }

    // <summary>
    // �̼� ����Ʈ (�̼� �̸�, �̼� ����, ���� �̸�, ���� ����)
    // </summary>
    public void MissionList()
    {
        Mission[] missionsToAdd = new Mission[]
        {
             new Mission("First Battle",
                         "Clear the first stage of the battlefield.",
                         1, Define.MissionType.StageClear, Define.RecallType.FriendTicket, 1),
             new Mission("Adaptation Phase",
                         "Clear the third stage of the battlefield.",
                         3, Define.MissionType.StageClear, Define.RecallType.FriendTicket, 3),
             new Mission("Now I too am an expert",
                         "Clear the tenth stage of the battlefield.",
                         10, Define.MissionType.StageClear, Define.RecallType.FriendTicket, 5),
             new Mission("Gacha is fun",
                         "Perform the regular gacha 10 times.",
                         10, Define.MissionType.GachaCount, Define.RecallType.Ticket1, 10),
             new Mission("Good Feeling",
                         "Perform the regular gacha 50 times.",
                         50, Define.MissionType.GachaCount, Define.RecallType.Ticket1, 10),
             new Mission("Gacha Addiction",
                         "Perform the regular gacha 100 times.",
                         100, Define.MissionType.GachaCount, Define.RecallType.Ticket1, 10),
             new Mission("Sprout",
                         "Raise any character's Total of level 5.",
                         5, Define.MissionType.LevelUp, Define.RecallType.Ticket2, 5),
             new Mission("Evolution",
                         "Raise any character's Total of level 10.",
                         10, Define.MissionType.LevelUp, Define.RecallType.Ticket2, 5),
             new Mission("Leveling Is Fun",
                         "Raise any character's Total of level 20.",
                         20, Define.MissionType.LevelUp, Define.RecallType.Ticket2, 5),
         };

        missions.AddRange(missionsToAdd);
    }

    // <summary>
    // ������ ���ؼ� �´ٸ� ���� ����
    // </summary>
    public void ConditionComparison(Define.MissionType type, int comparCount)
    {
        foreach (var mission in missions)
        {
            if (mission.missionType == type && !mission.isMissionComplete && mission.missionCount <= comparCount)
            { //�̼� Ÿ�� ��                  �̼� �������� ��            �̼� count��
                CheckRewardAndCheckMission(mission);
            }
        }
    }

    // <summary>
    // �̼� �Ϸ� �� ���� ���� �Լ�
    // </summary>
    private void CheckRewardAndCheckMission(Mission mission)
    {
        GiveReward(mission.itemReward, mission.rewardCount);
        mission.isMissionComplete = true;
        Debug.Log($"�̼� : {mission.missionName} �� {mission.isMissionComplete}");
    }

    // <summary>
    // �̼��� ������ ���� ���ް�, �̼� on/off ����
    // </summary>
    public void GiveReward(Define.RecallType reward, int rewardCount)
    {
        if (rewardActions.ContainsKey(reward))
        {
            rewardActions[reward].Invoke(rewardCount);
            Debug.Log($"���� : {reward}, �� {rewardCount} ��ŭ ����");
        }
    }
}