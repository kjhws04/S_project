using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// <summary>
// ���� ȭ�鿡�� �̼� ��ư�� ������ �� popup
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

        // �̼��� �Ϸ���� ���� ������� ����
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
