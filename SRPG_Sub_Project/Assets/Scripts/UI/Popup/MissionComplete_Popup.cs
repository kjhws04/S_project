using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// <summary>
// 어떠한 미션이 성공 되었을 때, 화면의 오른쪽 위에 popup
// </summary>
public class MissionComplete_Popup : UI_Popup
{
    WaitForSeconds wait;
    enum Texts
    {
        MissionTitle, 
        MissionDecs,
        RewardName
    }

    public void ShowPopup(Mission mission)
    {
        wait = new WaitForSeconds(3f);
        StartCoroutine(NoticeRoutine());

        Bind<TextMeshProUGUI>(typeof(Texts));
        GetTextMeshProUGUI((int)Texts.MissionTitle).text = $"{mission.missionName}";
        GetTextMeshProUGUI((int)Texts.MissionDecs).text = $"{mission.missionDesc}";
        GetTextMeshProUGUI((int)Texts.RewardName).text = $"{mission.itemReward} X {mission.rewardCount}";
    }

    IEnumerator NoticeRoutine()
    {
        yield return wait;
        Managers.UI.ClosePopupUI();
    }
}