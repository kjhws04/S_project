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

    public void MissionSetting(Mission mission)
    {
        #region Bind
        Bind<TextMeshProUGUI>(typeof(Texts));
        _userData = Managers.Game.GetUserData().GetComponent<UserData>();
        #endregion
        GetTextMeshProUGUI((int)Texts.Complete).text = mission.isMissionComplete ? "Completed" : "Not Complete";
        if (mission.isMissionComplete)
            CompleteMissions();

        GetTextMeshProUGUI((int)Texts.Title).text = $"{mission.missionName}";
        GetTextMeshProUGUI((int)Texts.Desc).text = $"{mission.missionDesc}";
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

    private void CompleteMissions()
    {
        gameObject.GetComponent<Image>().color = Color.yellow;
        return;
    }
}
