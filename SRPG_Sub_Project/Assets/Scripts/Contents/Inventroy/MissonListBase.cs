using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissonListBase : UI_Base
{
    UserData _userData;

    enum Texts
    {
        Title,
        Desc,
        Score,
        Reward,
        Complete
    }

    public override void Init()
    {

    }

    // <summary>
    // 미션창을 init할 때, 미션창에 필요한 정보를 표시하는 함수
    // (성공 여부/미션 이름,설명/달성목표,현재목표 카운트/보상 아이템 이름, 개수)
    // </summary>
    public void MissionSetting(Mission mission)
    {
        #region Bind
        Bind<TextMeshProUGUI>(typeof(Texts));
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        #endregion
        // 미션 성공 여부에 따른 text 차이
        GetTextMeshProUGUI((int)Texts.Complete).text = mission.isMissionComplete ? "Completed" : "Not Complete";
        // 성공한 미션은 따로 관리
        if (mission.isMissionComplete)
            CompleteMissions();

        GetTextMeshProUGUI((int)Texts.Title).text = $"{mission.missionName}";
        GetTextMeshProUGUI((int)Texts.Desc).text = $"{mission.missionDesc}";

        // 미션 타입에 따라 필요한 변수 대입
        switch (mission.missionType)
        {
            case Define.MissionType.Unknown:
                GetTextMeshProUGUI((int)Texts.Score).text = $"Unknown";
                break;
            case Define.MissionType.StageClear:
                GetTextMeshProUGUI((int)Texts.Score).text = $"{_userData.Stage} / {mission.missionCount}";
                break;
            case Define.MissionType.GachaCount:
                GetTextMeshProUGUI((int)Texts.Score).text = $"{_userData.Gacha} / {mission.missionCount}";
                break;
            case Define.MissionType.LevelUp:
                GetTextMeshProUGUI((int)Texts.Score).text = $"{_userData.LevelUp} / {mission.missionCount}";
                break;
        }
        GetTextMeshProUGUI((int)Texts.Reward).text = $"{mission.itemReward} X {mission.rewardCount}";
    }

    // <summary>
    // 성공한 미션은 base color를 노란색으로 변경
    // </summary>
    private void CompleteMissions()
    {
        gameObject.GetComponent<Image>().color = Color.yellow;
        return;
    }
}
