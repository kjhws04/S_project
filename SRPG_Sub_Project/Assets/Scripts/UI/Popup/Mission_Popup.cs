using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// <summary>
// 메인 화면에서 미션 버튼을 눌렸을 때 popup
// </summary>
public class Mission_Popup : UI_Popup
{
    public GameObject miBase;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        List<Mission> missions = Managers.Mission.missions;

        // 미션이 완료되지 않은 순서대로 정렬
        missions = missions.OrderBy(missions => missions.isMissionComplete).ToList();

        foreach (var mission in missions)
        {
            GameObject go = Managers.Resource.Instantiate("Slot/MissionListBase");
            go.transform.SetParent(miBase.transform);
            go.GetComponent<MissonListBase>().MissionSetting(mission);
        }
    }

    public void BtnExit()
    {
        Managers.UI.ClosePopupUI();
    }
}
