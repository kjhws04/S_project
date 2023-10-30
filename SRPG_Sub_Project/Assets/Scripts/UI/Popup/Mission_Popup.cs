using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        // 완료되지 않은 순서대로 정렬
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
