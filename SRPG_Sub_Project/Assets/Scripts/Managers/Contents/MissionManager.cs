using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// 기본 미션 구성 함수
// </summary>
public class Mission
{
    public string missionName; //미션 이름
    public string missionDesc; //미션 설명
    public int missionCount; //미션에서 설명하고 있는 ob의 카운트
    public Define.MissionType missionType; //미션 타입
    public Define.RecallType itemReward; //보상 이름
    public int rewardCount; //보상 개수

    public bool isMissionComplete; //클리어 여부

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

//BattleManager => Win() : 스테이지 관련 미션
//Recall_Base => BtnRecall1(10)Time() : 가챠 관련 미션
//LevelUp_Popup => StatSetting() : 레벨업 관련 미션
public class MissionManager
{
    //미션 목록을 가지고 있는 변수
    public List<Mission> missions = new List<Mission>();
    //리워드 목록을 가질 변수
    private Dictionary<Define.RecallType, Action<int>> rewardActions = new Dictionary<Define.RecallType, Action<int>>();

    UserData _userData;

    public void Init()
    {
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        MissionList();

        //미리 userData에 보상 저장
        rewardActions.Add(Define.RecallType.Ticket1, (rewardCount) => _userData.Ticket1 += rewardCount);
        rewardActions.Add(Define.RecallType.Ticket2, (rewardCount) => _userData.Ticket2 += rewardCount);
        rewardActions.Add(Define.RecallType.FriendTicket, (rewardCount) => _userData.TicketFriend += rewardCount);
    }

    // <summary>
    // 미션 리스트 (미션 이름, 미션 내용, 보상 이름, 보상 개수)
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
    // 조건을 비교해서 맞다면 보상 지급
    // </summary>
    public void ConditionComparison(Define.MissionType type, int comparCount)
    {
        foreach (var mission in missions)
        {
            if (mission.missionType == type && !mission.isMissionComplete && mission.missionCount <= comparCount)
            { //미션 타입 비교                  미션 성공여부 비교            미션 count비교
                CheckRewardAndCheckMission(mission);
            }
        }
    }

    // <summary>
    // 미션 완료 및 보상 지급 함수
    // </summary>
    private void CheckRewardAndCheckMission(Mission mission)
    {
        GiveReward(mission.itemReward, mission.rewardCount);
        mission.isMissionComplete = true;
        Debug.Log($"미션 : {mission.missionName} 가 {mission.isMissionComplete}");
    }

    // <summary>
    // 미션을 성공시 보상 지급과, 미션 on/off 관여
    // </summary>
    public void GiveReward(Define.RecallType reward, int rewardCount)
    {
        if (rewardActions.ContainsKey(reward))
        {
            rewardActions[reward].Invoke(rewardCount);
            Debug.Log($"보상 : {reward}, 를 {rewardCount} 만큼 지급");
        }
    }
}